using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ryze
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

            foreach (var minion in Targets.Minions)
            {
                /// <summary>
                ///     The LaneClear E Logic.
                /// </summary>
                if (Targets.Minions.Any(m => !m.HasBuff("RyzeE")) &&
                    Targets.Minions.Any(
                        m =>
                            m.HasBuff("RyzeE") &&
                            m.Distance(Targets.Minions.FirstOrDefault(m2 => !m2.HasBuff("RyzeE"))) < 200))
                {
                    if (Vars.E.IsReady() &&
                        minion.IsValidTarget(Vars.E.Range) &&
                        GameObjects.Player.ManaPercent >
                            ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                        Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                    {
                        Vars.E.CastOnUnit(Targets.Minions.FirstOrDefault(
                            m =>
                                m.HasBuff("RyzeE") &&
                                m.Distance(Targets.Minions.FirstOrDefault(m2 => !m2.HasBuff("RyzeE"))) < 200));
                    }
                }
                else
                {
                    /// <summary>
                    ///     The LaneClear Q Logic.
                    /// </summary>
                    if (Vars.Q.IsReady() &&
                        minion.IsValidTarget(Vars.Q.Range) &&
                        GameObjects.Player.ManaPercent >
                            ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["laneclear"]) &&
                        Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                    {
                        Vars.Q.Cast(minion.ServerPosition);
                    }
                }
            }

            foreach (var minion in Targets.JungleMinions)
            {
                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                if (Targets.JungleMinions.Any(m => !m.HasBuff("RyzeE")))
                {
                    if (Vars.E.IsReady() &&
                        minion.IsValidTarget(Vars.E.Range) &&
                        GameObjects.Player.ManaPercent >
                            ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                        Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                    {
                        Vars.E.CastOnUnit(minion);
                    }
                }
                else
                {
                    /// <summary>
                    ///     The JungleClear Q Logic.
                    /// </summary>
                    if (Vars.Q.IsReady() &&
                        minion.IsValidTarget(Vars.Q.Range) &&
                        GameObjects.Player.ManaPercent >
                            ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
                        Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                    {
                        Vars.Q.Cast(minion.ServerPosition);
                    }
                }
            }
        }
    }
}