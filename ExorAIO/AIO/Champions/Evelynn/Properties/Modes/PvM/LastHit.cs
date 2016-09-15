
#pragma warning disable 1587

namespace ExorAIO.Champions.Evelynn
{
    using System;
    using System.Linq;

    using Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;

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
        public static void LastHit(EventArgs args)
        {
            /// <summary>
            ///     The Q LastHit Logic.
            /// </summary>
            if (Vars.Q.IsReady()
                && GameObjects.Player.ManaPercent
                > ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["lasthit"])
                && Vars.Menu["spells"]["q"]["lasthit"].GetValue<MenuSliderButton>().BValue)
            {
                if (
                    Targets.Minions.Any(
                        m => Vars.GetRealHealth(m) < (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                {
                    Vars.Q.Cast();
                }
            }
        }

        #endregion
    }
}