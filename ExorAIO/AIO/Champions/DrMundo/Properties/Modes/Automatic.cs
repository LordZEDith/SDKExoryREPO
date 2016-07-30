using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.DrMundo
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
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The Automatic Q LastHit Logics.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo &&
                GameObjects.Player.HealthPercent >
                ManaManager.GetNeededHealth(Vars.Q.Slot, Vars.Menu["spells"]["q"]["logical"]) &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var minion in GameObjects.EnemyMinions.Where(
                    m =>
                        m.IsValidTarget(Vars.Q.Range) &&
                        !m.IsValidTarget(Vars.AARange) &&
                        Vars.GetRealHealth(m) <
                        (float) GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                {
                    if (!Vars.Q.GetPrediction(minion).CollisionObjects.Any(c => Targets.Minions.Contains(c)))
                    {
                        Vars.Q.Cast(minion.ServerPosition);
                    }
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady())
            {
                /// <summary>
                ///     If the player doesn't have the W Buff.
                /// </summary>
                if (!GameObjects.Player.HasBuff("BurningAgony"))
                {
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        /// <summary>
                        ///     LaneClear W enable logic. 
                        /// </summary>
                        case OrbwalkingMode.LaneClear:
                            if (GameObjects.Player.HealthPercent >=
                                ManaManager.GetNeededHealth(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) &&
                                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>().BValue)
                            {
                                if (Targets.JungleMinions.Any() ||
                                    Targets.Minions.Count() >= 2)
                                {
                                    Vars.W.Cast();
                                }
                            }
                            break;

                        /// <summary>
                        ///     Combo W enable logic. 
                        /// </summary>
                        case OrbwalkingMode.Combo:
                            if (GameObjects.Player.CountEnemyHeroesInRange(Vars.W.Range) > 0 &&
                                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
                            {
                                Vars.W.Cast();
                            }
                            break;

                        default:
                            break;
                    }
                }

                /// <summary>
                ///     If the player has the W Buff.
                /// </summary>
                else
                {
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        /// <summary>
                        ///     LaneClear Combo disable logic. 
                        /// </summary>
                        case OrbwalkingMode.LaneClear:
                            if (GameObjects.Player.HealthPercent <
                                ManaManager.GetNeededHealth(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) ||
                                !Targets.JungleMinions.Any() && Targets.Minions.Count() < 2 ||
                                !Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>().BValue)
                            {
                                Vars.W.Cast();
                            }
                            break;

                        /// <summary>
                        ///     General disable logic. 
                        /// </summary>
                        default:
                            if (GameObjects.Player.CountEnemyHeroesInRange(Vars.W.Range) == 0 ||
                                !Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
                            {
                                Vars.W.Cast();
                            }
                            break;
                    }
                }
            }

            /// <summary>
            ///     The R Lifesaver Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(700) > 0 &&
                Vars.Menu["spells"]["r"]["lifesaver"].GetValue<MenuBool>().Value &&
                Health.GetPrediction(GameObjects.Player, (int) (250 + Game.Ping/2f)) <= GameObjects.Player.MaxHealth/5)
            {
                Vars.R.Cast();
            }
        }
    }
}