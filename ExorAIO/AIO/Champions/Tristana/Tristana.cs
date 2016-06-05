using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Tristana
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Tristana
    {
        /// <summary>
        ///     Loads Tristana.
        /// </summary>
        public void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

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
            ///     Updates the spells.
            /// </summary>
            Spells.Initialize();

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
                    Logics.BuildingClear(args);
                    break;

                default:
                    break;
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
                Vars.W.IsReady() &&
                args.Buff.Caster as Obj_AI_Hero != null &&
                Vars.Menu["spells"]["w"]["antigrab"].GetValue<MenuBool>().Value)
            {
                if (args.Buff.Name.Equals("ThreshQ") ||
                    args.Buff.Name.Equals("rocketgrab2"))
                {
                    Vars.W.Cast(GameObjects.Player.ServerPosition.Extend((args.Buff.Caster as Obj_AI_Hero).ServerPosition, -Vars.W.Range));
                }
            }
        }
    }
}