using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace ExorAIO.Champions.Cassiopeia
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
        public static void Harass(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() || Invulnerable.Check(Targets.Target, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range) &&
                GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["harass"]) &&
                Vars.Menu["spells"]["q"]["harass"].GetValue<MenuSliderButton>()
                                                  .BValue)
            {
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target)
                                .CastPosition);
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            DelayAction.Add(1000,
                () =>
                {
                    if (Vars.W.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range) &&
                        GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["harass"]) &&
                        Vars.Menu["spells"]["w"]["harass"].GetValue<MenuSliderButton>()
                                                          .BValue)
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(Targets.Target)
                                        .CastPosition);
                    }
                });
        }
    }
}
