using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jhin
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
            ///     The Clear W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                /// <summary>
                ///     The LaneClear W Logic.
                /// </summary>
                if (Vars.W.GetLineFarmLocation(Targets.Minions, Vars.W.Width).MinionsHit >= 4)
                {
                    Vars.W.Cast(Vars.W.GetLineFarmLocation(Targets.Minions, Vars.W.Width).Position);
                }

                /// <summary>
                ///     The JungleClear W Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.W.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["clear"]) &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                /// <summary>
                ///     The LaneClear Q Logic.
                /// </summary>
                if (Targets.Minions.Any() &&
                    Targets.Minions.Count() >= 3)
                {
                    if (Targets.Minions.Where(
                        m =>
                            m.IsValidTarget(Vars.Q.Range)).Sum(
                                h =>
                                    (int)(h.Health / (float)GameObjects.Player.GetSpellDamage(h, SpellSlot.Q))) >= 3)
                    {
                        Vars.Q.CastOnUnit(Targets.Minions.OrderBy(m => Vars.GetRealHealth(m)).First());
                    }
                }

                /// <summary>
                ///     The JungleClear Q Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
                }
                return;
            }
        }
    }
}