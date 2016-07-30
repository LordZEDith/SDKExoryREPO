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

namespace ExorAIO.Champions.MissFortune
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class MissFortune
    {
        /// <summary>
        ///     Loads Miss Fortune.
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

            /// <summary>
            ///     Initializes the cone drawings.
            /// </summary>
            ConeDrawings.Initialize();
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

            if (GameObjects.Player.HasBuff("missfortunebulletsound"))
            {
                return;
            }

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
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                if (AutoAttack.IsAutoAttack(args.SData.Name))
                {
                    /// <summary>
                    ///     Initializes the orbwalkingmodes.
                    /// </summary>
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        case OrbwalkingMode.Combo:
                            Logics.Weaving(sender, args);
                            break;

                        case OrbwalkingMode.LaneClear:
                            Logics.JungleClear(sender, args);
                            Logics.BuildingClear(sender, args);
                            break;

                        default:
                            break;
                    }

                    Vars.PassiveTarget = args.Target as AttackableUnit;
                }
                else
                {
                    switch (args.SData.Name)
                    {
                        case "MissFortuneRicochetShot":
                            //case "MissFortuneRicochetShotMissile":
                            Vars.PassiveTarget = args.Target as AttackableUnit;
                            break;

                        default:
                            break;
                    }
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
            if (Vars.E.IsReady() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(args.End);
            }
        }

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="OrbwalkingActionArgs" /> instance containing the event data.</param>
        public static void OnAction(object sender, OrbwalkingActionArgs args)
        {
            /// <summary>
            ///     Stop attack commands while channeling R.
            /// </summary>
            args.Process = !GameObjects.Player.HasBuff("missfortunebulletsound");

            switch (args.Type)
            {
                case OrbwalkingType.BeforeAttack:

                    /// <summary>
                    ///     The Target Switching Logic (Passive Stacks).
                    /// </summary>
                    if (args.Target is Obj_AI_Hero &&
                        args.Target.NetworkId == Vars.PassiveTarget.NetworkId &&
                        Vars.Menu["miscellaneous"]["passive"].GetValue<MenuBool>().Value)
                    {
                        if (Vars.GetRealHealth(args.Target as Obj_AI_Hero) >
                            GameObjects.Player.GetAutoAttackDamage(args.Target as Obj_AI_Hero)*3)
                        {
                            if (GameObjects.EnemyHeroes.Any(
                                t =>
                                    t.IsValidTarget(Vars.AARange) &&
                                    t.NetworkId != Vars.PassiveTarget.NetworkId))
                            {
                                args.Process = false;
                                Variables.Orbwalker.ForceTarget = GameObjects.EnemyHeroes.Where(
                                    t =>
                                        t.IsValidTarget(Vars.AARange) &&
                                        t.NetworkId != Vars.PassiveTarget.NetworkId).OrderByDescending(
                                            o =>
                                                Data.Get<ChampionPriorityData>().GetPriority(o.ChampionName)).First();
                                return;
                            }

                            Variables.Orbwalker.ForceTarget = null;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }
}