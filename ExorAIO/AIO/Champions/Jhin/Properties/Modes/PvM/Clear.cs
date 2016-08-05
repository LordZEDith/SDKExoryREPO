
#pragma warning disable 1587

namespace ExorAIO.Champions.Jhin
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        #region Public Methods and Operators

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
            if (Vars.W.IsReady()
                && GameObjects.Player.ManaPercent
                > ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["laneclear"])
                && Vars.Menu["spells"]["w"]["laneclear"].GetValue<MenuSliderButton>().BValue)
            {
                if (GameObjects.EnemyHeroes.Any(t => !Invulnerable.Check(t) && t.IsValidTarget(Vars.W.Range - 100f)))
                {
                    return;
                }

                if (Vars.W.GetLineFarmLocation(Targets.Minions, Vars.W.Width).MinionsHit >= 4)
                {
                    Vars.W.Cast(Vars.W.GetLineFarmLocation(Targets.Minions, Vars.W.Width).Position);
                }
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady()
                && GameObjects.Player.ManaPercent
                > ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["clear"])
                && (GameObjects.Player.HasBuff("JhinPassiveReload")
                    || !Vars.Menu["spells"]["q"]["reloadclear"].GetValue<MenuBool>().Value)
                && Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                /// <summary>
                ///     The LaneClear Q Logic.
                /// </summary>
                if (Targets.Minions.Any())
                {
                    foreach (var minion in Targets.Minions.Where(
                        m =>
                        m.IsValidTarget(Vars.Q.Range) && Targets.Minions.Count(m2 => m2.Distance(m) < 350f) >= 3
                        && Vars.GetRealHealth(m) < (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                    {
                        Vars.Q.CastOnUnit(minion);
                    }
                }

                /// <summary>
                ///     The JungleClear Q Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
                }
            }
        }

        #endregion
    }
}