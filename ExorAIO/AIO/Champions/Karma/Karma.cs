using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Karma
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Karma
    {
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

                default:
                    break;
            }
        }

        /// <summary>
        ///     Called when a <see cref="AttackableUnit" /> takes/gives damage.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AttackableUnitDamageEventArgs" /> instance containing the event data.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender as Obj_AI_Hero == null && sender as Obj_AI_Turret == null &&
                !Targets.JungleMinions.Contains(sender as Obj_AI_Minion))
            {
                return;
            }

            if (sender.IsAlly || args.Target as Obj_AI_Hero == null || !(args.Target as Obj_AI_Hero).IsAlly)
            {
                return;
            }

            if (Vars.E.IsReady() && (args.Target as Obj_AI_Hero).IsValidTarget(Vars.E.Range, false) &&
                Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["e"]["whitelist"][(args.Target as Obj_AI_Hero).ChampionName.ToLower()]
                    .GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(args.Target as Obj_AI_Hero);
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Vars.E.IsReady() && GameObjects.Player.Distance(args.End) < 750 &&
                Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["empe"].GetValue<MenuBool>().Value &&
                    GameObjects.AllyHeroes.Count(a => a.IsValidTarget(600f, false)) >= 2)
                {
                    Vars.R.Cast();
                }

                Vars.E.Cast();
            }
        }

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
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        case OrbwalkingMode.Hybrid:
                        case OrbwalkingMode.LastHit:
                        case OrbwalkingMode.LaneClear:

                            /// <summary>
                            ///     The 'Support Mode' Logic.
                            /// </summary>
                            if (Vars.Menu["miscellaneous"]["support"].GetValue<MenuBool>().Value)
                            {
                                if (args.Target is Obj_AI_Minion &&
                                    GameObjects.AllyHeroes.Any(a => a.Distance(GameObjects.Player) < 2500))
                                {
                                    args.Process = false;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}