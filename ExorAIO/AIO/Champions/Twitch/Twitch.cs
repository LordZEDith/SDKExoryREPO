using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Twitch
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Twitch
    {
        /// <summary>
        ///     Loads Twitch.
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
                        Logics.BuildingClear(sender, args);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///     Called on spell cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SpellbookCastSpellEventArgs" /> instance containing the event data.</param>
        public static void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (sender.Owner.IsMe &&
                GameObjects.Player.Spellbook.GetSpell(args.Slot).Name.Equals("recall") &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
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
                    if (!GameObjects.Player.IsUnderEnemyTurret() &&
                        GameObjects.Player.HasBuff("TwitchHideInShadows"))
                    {
                        if (GameObjects.Player.GetBuff("TwitchHideInShadows").EndTime - Game.Time >
                            GameObjects.Player.GetBuff("TwitchHideInShadows").EndTime - GameObjects.Player.GetBuff("TwitchHideInShadows").StartTime -
                            Vars.Menu["miscellaneous"]["stealthtime"].GetValue<MenuSlider>().Value/1000)
                        {
                            args.Process = false;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }
}