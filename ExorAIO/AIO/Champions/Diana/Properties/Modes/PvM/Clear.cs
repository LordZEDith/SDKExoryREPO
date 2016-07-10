using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Diana
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Q LaneClear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Minions.Any() &&
                GameObjects.Player.ManaPercent > 
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["laneclear"]) &&
                Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuSliderButton>().BValue &&
                Vars.Q.GetLineFarmLocation(Targets.Minions, Vars.Q.Width).MinionsHit >= 4)
            {
                Vars.Q.Cast(Vars.Q.GetLineFarmLocation(Targets.Minions, Vars.Q.Width).Position);
            }

            /// <summary>
            ///     The W LaneClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Targets.Minions.Any() &&
                GameObjects.Player.ManaPercent > 
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["laneclear"]) &&
                Vars.Menu["spells"]["w"]["laneclear"].GetValue<MenuSliderButton>().BValue)
            {
                if (Targets.Minions.Count(m => m.IsValidTarget(Vars.W.Range)) >= 3)
                {
                    Vars.W.Cast();
                }
            }

        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void JungleClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is Obj_AI_Minion) ||
                !Targets.JungleMinions.Contains(args.Target as Obj_AI_Minion))
            {
                return;
            }

            /// <summary>
            ///     The Q JungleClear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > 
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
                Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.Q.Cast((args.Target as Obj_AI_Minion).ServerPosition);
                return;
            }

            /// <summary>
            ///     The W JungleClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent > 
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["jungleclear"]) &&
                Vars.Menu["spells"]["w"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.W.Cast();
            }
        }
    }
}