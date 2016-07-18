using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;
using LeagueSharp.Data.Enumerations;
using LeagueSharp.Data;
using LeagueSharp.Data.DataTypes;

namespace ExorAIO.Champions.Kalista
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Kalista
    {
        /// <summary>
        ///     Loads Kalista.
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
            ///     Initializes the damage drawings.
            /// </summary>
            Healthbars.Initialize();
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

            if (GameObjects.Player.IsWindingUp ||
                GameObjects.Player.IsDashing())
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
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="OrbwalkingActionArgs" /> instance containing the event data.</param>
        public static void OnAction(object sender, OrbwalkingActionArgs args)
        {
            switch (args.Type)
            {
                case OrbwalkingType.BeforeAttack:

                    /// <summary>
                    ///     The Target Forcing Logic.
                    /// </summary>
                    if (Vars.GetRealHealth(args.Target as Obj_AI_Hero) >
                            GameObjects.Player.GetAutoAttackDamage(args.Target as Obj_AI_Hero) * 3)
                    {
                        if (GameObjects.EnemyHeroes.Any(
                            t =>
                                t.IsValidTarget(Vars.AARange) &&
                                t.HasBuff("kalistacoopstrikemarkally")))
                        {
                            args.Process = false;
                            Variables.Orbwalker.ForceTarget = GameObjects.EnemyHeroes.Where(
                                t =>
                                    t.IsValidTarget(Vars.AARange) &&
                                    t.HasBuff("kalistacoopstrikemarkally")).OrderByDescending(
                                        o =>
                                            Data.Get<ChampionPriorityData>().GetPriority(o.ChampionName)).First();
                            return;
                        }

                        Variables.Orbwalker.ForceTarget = null;
                    }
                    break;

                case OrbwalkingType.NonKillableMinion:

                    /// <summary>
                    ///     The E against Non-Killable Minions Logic.
                    /// </summary>
                    if (Vars.E.IsReady() &&
                        Bools.IsPerfectRendTarget(args.Target as Obj_AI_Minion) &&
                        Vars.GetRealHealth(args.Target as Obj_AI_Minion) <
                            (float)GameObjects.Player.GetSpellDamage(args.Target as Obj_AI_Minion, SpellSlot.E) +
                            (float)GameObjects.Player.GetSpellDamage(args.Target as Obj_AI_Minion, SpellSlot.E, DamageStage.Buff))
                    {
                        Vars.E.Cast();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}