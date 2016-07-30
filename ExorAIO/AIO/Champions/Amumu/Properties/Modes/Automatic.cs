using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Amumu
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
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["logical"].GetValue<MenuSliderButton>().BValue)
            {
                /// <summary>
                ///     If the player doesn't have the W Buff.
                /// </summary>
                if (!GameObjects.Player.HasBuff("AuraOfDespair"))
                {
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        /// <summary>
                        ///     The Q Combo Enable Logic.
                        /// </summary>
                        case OrbwalkingMode.Combo:

                            if (GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.W.Range)))
                            {
                                Vars.W.Cast();
                            }
                            break;

                        /// <summary>
                        ///     The W Clear Enable Logic.
                        /// </summary>
                        case OrbwalkingMode.LaneClear:

                            if (GameObjects.Player.ManaPercent >=
                                ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["logical"]) &&
                                (Targets.Minions.Count() >= 2 || Targets.JungleMinions.Any()))
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
                        ///     The W Clear Disable Logic.
                        /// </summary>
                        case OrbwalkingMode.LaneClear:

                            if (GameObjects.Player.ManaPercent <
                                ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["logical"]) ||
                                (Targets.Minions.Count() < 2 && !Targets.JungleMinions.Any()))
                            {
                                Vars.W.Cast();
                            }
                            break;

                        /// <summary>
                        ///     The Default Disable Logic.
                        /// </summary>
                        default:

                            if (!GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.W.Range)))
                            {
                                Vars.W.Cast();
                            }
                            break;
                    }
                }
            }
        }
    }
}