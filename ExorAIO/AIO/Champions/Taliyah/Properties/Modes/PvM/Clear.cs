using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

#pragma warning disable 1587

namespace ExorAIO.Champions.Taliyah
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
            ///     The Clear E Logic.
            /// </summary>
            if (Vars.E.IsReady())
            {
                /// <summary>
                ///     The LaneClear E Logic.
                /// </summary>
                if (Targets.Minions.Any() && Targets.Minions.Count(m => m.IsValidTarget(Vars.E.Range)) >= 3 &&
                    GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                    Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>()
                                                         .BValue && Vars.E.GetCircularFarmLocation(Targets.Minions, Vars.E.Width)
                                                                        .MinionsHit >= 3)
                {
                    Vars.E.Cast(Vars.E.GetCircularFarmLocation(Targets.Minions, Vars.E.Width)
                                    .Position);
                }

                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any(m => m.IsValidTarget(Vars.E.Range)) &&
                    GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                    Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>()
                                                           .BValue)
                {
                    Vars.E.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady())
            {
                /// <summary>
                ///     The LaneClear Q Logic.
                /// </summary>
                if (Targets.Minions.Any() &&
                    GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["laneclear"]) &&
                    Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuSliderButton>()
                                                         .BValue)
                {
                    if (Taliyah.TerrainObject != null && Vars.Menu["spells"]["q"]["q2"]["laneclearfull"].GetValue<MenuBool>()
                                                                                                        .Value)
                    {
                        return;
                    }

                    if (Vars.Q.GetLineFarmLocation(Targets.Minions, Vars.Q.Width)
                            .MinionsHit >= 3)
                    {
                        Vars.Q.Cast(Vars.Q.GetLineFarmLocation(Targets.Minions, Vars.Q.Width)
                                        .Position);
                    }
                    else if (Vars.Q.GetCircularFarmLocation(Targets.Minions, Vars.Q.Width)
                                 .MinionsHit >= 3)
                    {
                        Vars.Q.Cast(Vars.Q.GetCircularFarmLocation(Targets.Minions, Vars.Q.Width)
                                        .Position);
                    }
                }

                /// <summary>
                ///     The JungleClear Q Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
                    Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>()
                                                           .BValue)
                {
                    if (Taliyah.TerrainObject != null && Vars.Menu["spells"]["q"]["q2"]["jungleclearfull"].GetValue<MenuBool>()
                                                                                                          .Value)
                    {
                        return;
                    }

                    Vars.Q.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }
        }
    }
}
