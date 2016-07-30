using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Quinn
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
                Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuSliderButton>().BValue)
            {
                if (Targets.Minions.Count(m => m.Distance(Targets.Minions[0]) < 200f) >= 3)
                {
                    Vars.Q.Cast(Targets.Minions[0].ServerPosition);
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
            if (!(Variables.Orbwalker.GetTarget() is Obj_AI_Minion) ||
                !Targets.JungleMinions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion))
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
                Vars.Q.Cast((Variables.Orbwalker.GetTarget() as Obj_AI_Minion).ServerPosition);
                return;
            }

            /// <summary>
            ///     The E JungleClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent >
                ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.CastOnUnit(Variables.Orbwalker.GetTarget() as Obj_AI_Minion);
            }
        }
    }
}