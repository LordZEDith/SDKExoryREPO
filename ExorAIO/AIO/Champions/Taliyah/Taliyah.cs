
#pragma warning disable 1587

namespace ExorAIO.Champions.Taliyah
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Taliyah
    {
        #region Static Fields

        /// <summary>
        ///     Defines the Terrain object.
        /// </summary>
        public static GameObject TerrainObject;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Called when an object gets created by the game.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnCreate(GameObject obj, EventArgs args)
        {
            if (obj.IsValid && obj.Name.Equals("Taliyah_Base_Q_aoe_bright.troy"))
            {
                TerrainObject = obj;
            }
        }

        /// <summary>
        ///     Called when an object gets deleted by the game.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnDelete(GameObject obj, EventArgs args)
        {
            if (obj.IsValid && obj.Name.Equals("Taliyah_Base_Q_aoe_bright.troy"))
            {
                DelayAction.Add(
                    500,
                    () =>
                        {
                            if (
                                !ObjectManager.Get<GameObject>()
                                     .Any(
                                         o =>
                                         o.IsAlly && o.Distance(GameObjects.Player) < 412.5f
                                         && o.Name.Equals("Taliyah_Base_Q_aoe_bright.troy")))
                            {
                                TerrainObject = null;
                            }
                        });
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (Vars.W.IsReady() && args.Sender.IsValidTarget(Vars.W.Range)
                && Vars.Menu["spells"]["w"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(
                    args.Sender.IsMelee
                        ? GameObjects.Player.ServerPosition.Extend(args.End, GameObjects.Player.Distance(args.End) * 2)
                        : GameObjects.Player.ServerPosition);
            }
            if (Vars.E.IsReady() && args.Sender.IsValidTarget(Vars.E.Range)
                && Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(args.End);
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (Vars.W.IsReady() && args.Sender.IsValidTarget(Vars.W.Range)
                && Vars.Menu["spells"]["w"]["interrupter"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(GameObjects.Player.ServerPosition);
            }
            if (Vars.E.IsReady() && args.Sender.IsValidTarget(Vars.E.Range)
                && Vars.Menu["spells"]["e"]["interrupter"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(args.Sender.ServerPosition);
            }
        }

        /// <summary>
        ///     Called while processing spellcast operations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            ///     Automatically Mount on R Logic.
            /// </summary>
            if (Vars.R.IsReady() && sender != null && sender.IsMe && args.Slot != SpellSlot.Unknown
                && args.Slot.Equals(SpellSlot.R)
                && Vars.Menu["miscellaneous"]["mountr"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast();
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
            ///     Initializes the spells.
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
        ///     Loads Taliyah.
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

        #endregion
    }
}