
#pragma warning disable 1587

namespace ExorAIO.Champions.Lux
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.Data;
    using LeagueSharp.Data.DataTypes;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Lux
    {
        #region Static Fields

        /// <summary>
        ///     Defines the missile object for the E.
        /// </summary>
        public static GameObject EMissile;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="OrbwalkingActionArgs" /> instance containing the event data.</param>
        public static void OnAction(object sender, OrbwalkingActionArgs args)
        {
            switch (args.Type)
            {
                case OrbwalkingType.BeforeAttack:

                    /// <summary>
                    ///     The Target Forcing Logic.
                    /// </summary>
                    var hero = args.Target as Obj_AI_Hero;
                    if (hero != null && Vars.GetRealHealth(hero) > GameObjects.Player.GetAutoAttackDamage(hero) * 3)
                    {
                        if (
                            GameObjects.EnemyHeroes.Any(
                                t => t.IsValidTarget(Vars.AARange) && t.HasBuff("luxilluminatingfraulein")))
                        {
                            args.Process = false;
                            Variables.Orbwalker.ForceTarget =
                                GameObjects.EnemyHeroes.Where(
                                    t => t.IsValidTarget(Vars.AARange) && t.HasBuff("luxilluminatingfraulein"))
                                    .OrderByDescending(
                                        o => Data.Get<ChampionPriorityData>().GetPriority(o.ChampionName))
                                    .First();
                            return;
                        }

                        Variables.Orbwalker.ForceTarget = null;
                    }

                    /// <summary>
                    ///     The 'Support Mode' Logic.
                    /// </summary>
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        case OrbwalkingMode.Hybrid:
                        case OrbwalkingMode.LastHit:
                        case OrbwalkingMode.LaneClear:
                            if (Vars.Menu["miscellaneous"]["support"].GetValue<MenuBool>().Value)
                            {
                                if (args.Target is Obj_AI_Minion
                                    && GameObjects.AllyHeroes.Any(a => a.Distance(GameObjects.Player) < 2500))
                                {
                                    args.Process = false;
                                }
                            }
                            break;
                    }

                    break;
            }
        }

        /// <summary>
        ///     Called when an object gets created by the game.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnCreate(GameObject obj, EventArgs args)
        {
            if (obj.IsValid)
            {
                /// <summary>
                ///     Defines the missile object for the E.
                /// </summary>
                if (obj.Name.Contains("Lux_Base_E_tar"))
                {
                    EMissile = obj;
                }
            }
        }

        /// <summary>
        ///     Called when an object gets deleted by the game.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnDelete(GameObject obj, EventArgs args)
        {
            if (obj.IsValid)
            {
                /// <summary>
                ///     Removes the missile object for the E.
                /// </summary>
                if (obj.Name.Contains("Lux_Base_E_tar"))
                {
                    EMissile = null;
                }
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Vars.Q.IsReady() && args.IsDirectedToPlayer && args.Sender.IsValidTarget(Vars.Q.Range)
                && !Invulnerable.Check(args.Sender, DamageType.Magical, false)
                && Vars.Menu["spells"]["q"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(args.Sender.ServerPosition);
            }
        }

        /// <summary>
        ///     Called when a <see cref="AttackableUnit" /> takes/gives damage.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AttackableUnitDamageEventArgs" /> instance containing the event data.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(sender is Obj_AI_Hero) && !(sender is Obj_AI_Turret)
                && !Targets.JungleMinions.Contains(sender as Obj_AI_Minion))
            {
                return;
            }
            if (sender.IsAlly || !(args.Target is Obj_AI_Hero) || !((Obj_AI_Hero)args.Target).IsAlly)
            {
                return;
            }

            if (Vars.W.IsReady() && ((Obj_AI_Hero)args.Target).IsValidTarget(Vars.W.Range, false)
                && Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value
                && Vars.Menu["spells"]["w"]["whitelist"][((Obj_AI_Hero)args.Target).ChampionName.ToLower()]
                       .GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(Vars.W.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Logics.Automatic(args);

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Logics.Killsteal(args);
            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Logics.Combo(args);
                    break;
                case OrbwalkingMode.Hybrid:
                    Logics.Harass(args);
                    break;
                case OrbwalkingMode.LaneClear:
                    Logics.Clear(args);
                    break;
            }
        }

        /// <summary>
        ///     Loads Lux.
        /// </summary>
        public void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            Spells.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();

            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            Drawings.Initialize();
        }

        #endregion
    }
}