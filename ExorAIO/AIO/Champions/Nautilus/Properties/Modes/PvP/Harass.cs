
#pragma warning disable 1587

namespace ExorAIO.Champions.Nautilus
{
    using System;

    using Utilities;

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
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Harass(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() || Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The E Harass Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.Target.IsValidTarget(Vars.E.Range)
                && GameObjects.Player.ManaPercent
                > ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["harass"])
                && Vars.Menu["spells"]["e"]["harass"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.Cast();
            }
        }

        #endregion
    }
}