using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Ezreal
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Ezreal
    {
        /// <summary>
        ///     Loads Ezreal.
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

                case OrbwalkingMode.LastHit:
                    Logics.LastHit(args);
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
            if (sender.IsMe &&
                AutoAttack.IsAutoAttack(args.SData.Name))
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
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///     Fired when a buff is added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseBuffAddEventArgs" /> instance containing the event data.</param>
        public static void OnBuffAdd(Obj_AI_Base sender, Obj_AI_BaseBuffAddEventArgs args)
        {
            if (sender.IsMe &&
                Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["antigrab"].GetValue<MenuBool>().Value)
            {
                if (args.Buff.Name.Equals("ThreshQ") ||
                    args.Buff.Name.Equals("rocketgrab2"))
                {
                    Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(GameObjects.Player.ServerPosition,
                        -Vars.E.Range));
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
                args.Sender.IsMelee &&
                args.IsDirectedToPlayer &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(args.Sender.ServerPosition, -Vars.E.Range));
            }
        }
    }
}