using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Taliyah
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Taliyah
    {
        /// <summary>
        ///     Defines the Terrain object.
        /// </summary>
        public static bool IsOnTerrain;

        /// <summary>
        ///     Defines the check for the Terrain object.
        /// </summary>
        public static int TerrainLastCheckTick;

        /// <summary>
        ///     Loads Taliyah.
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
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Vars.W.IsReady() &&
                args.Sender.IsValidTarget(Vars.W.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["w"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(
                    args.Sender.ServerPosition,
                    args.Sender.IsFacing(GameObjects.Player) &&
                    GameObjects.Player.Distance(args.Sender) < Vars.AARange/2
                        ? GameObjects.Player.ServerPosition.Extend(args.Sender.ServerPosition, GameObjects.Player.Distance(args.Sender)*2)
                        : GameObjects.Player.ServerPosition);

                if (Vars.E.IsReady() &&
                    Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
                {
                    Vars.E.Cast(args.Sender.ServerPosition);
                }
                return;
            }

            if (Vars.E.IsReady() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical) &&
                Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(args.Sender.ServerPosition);
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (Vars.W.IsReady() &&
                args.Sender.IsValidTarget(Vars.W.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["w"]["interrupter"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(
                    args.Sender.ServerPosition,
                    args.Sender.IsFacing(GameObjects.Player) &&
                    GameObjects.Player.Distance(args.Sender) < Vars.AARange/2
                        ? GameObjects.Player.ServerPosition.Extend(args.Sender.ServerPosition, GameObjects.Player.Distance(args.Sender)*2)
                        : GameObjects.Player.ServerPosition);

                if (Vars.E.IsReady() &&
                    Vars.Menu["spells"]["e"]["interrupter"].GetValue<MenuBool>().Value)
                {
                    Vars.E.Cast(args.Sender.ServerPosition);
                }
                return;
            }

            if (Vars.E.IsReady() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical) &&
                Vars.Menu["spells"]["e"]["interrupter"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(args.Sender.ServerPosition);
            }
        }
    }
}