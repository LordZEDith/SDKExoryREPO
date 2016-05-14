using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Warwick
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff() ||
                !(Variables.Orbwalker.GetTarget() as Obj_AI_Minion).IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The W Clear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.IsWindingUp &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.W.Cast();
            }
            
            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     The Q Clear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["clear"]) &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                if (GameObjects.Player.MaxHealth >
                        GameObjects.Player.Health +
                        (float)GameObjects.Player.GetSpellDamage(Variables.Orbwalker.GetTarget() as Obj_AI_Minion, SpellSlot.Q) * 0.8)
                {
                    Vars.Q.CastOnUnit(Variables.Orbwalker.GetTarget() as Obj_AI_Minion);
                }
            }
        }
    }
}