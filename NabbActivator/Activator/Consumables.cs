
#pragma warning disable 1587

namespace NabbActivator
{
    using System;

    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;

    /// <summary>
    ///     The activator class.
    /// </summary>
    internal partial class Activator
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Consumables(EventArgs args)
        {
            if (GameObjects.Player.InFountain() || GameObjects.Player.IsRecalling()
                || !Vars.Menu["potions"].GetValue<MenuBool>().Value)
            {
                return;
            }
            if (!Bools.IsHealthPotRunning())
            {
                /// <summary>
                ///     The Refillable Potion Logic.
                /// </summary>
                if (Items.CanUseItem(2031) && GameObjects.Player.HealthPercent < Managers.MinHealthPercent)
                {
                    Items.UseItem(2031);
                    return;
                }

                /// <summary>
                ///     The Total Biscuit of Rejuvenation Logic.
                /// </summary>
                if (Items.CanUseItem(2010) && GameObjects.Player.HealthPercent < Managers.MinHealthPercent)
                {
                    Items.UseItem(2010);
                    return;
                }

                /// <summary>
                ///     The Health Potion Logic.
                /// </summary>
                if (Items.CanUseItem(2003) && GameObjects.Player.HealthPercent < Managers.MinHealthPercent)
                {
                    Items.UseItem(2003);
                }
            }

            if (GameObjects.Player.MaxMana < 200)
            {
                return;
            }

            /// <summary>
            ///     The Hunter's Potion Logic.
            /// </summary>
            if (Items.CanUseItem(2032))
            {
                if (!Bools.IsHealthPotRunning() && GameObjects.Player.HealthPercent < Managers.MinHealthPercent)
                {
                    Items.UseItem(2032);
                }
                else if (!Bools.IsManaPotRunning() && GameObjects.Player.ManaPercent < Managers.MinManaPercent)
                {
                    Items.UseItem(2032);
                }
            }

            /// <summary>
            ///     The Corrupting Potion Logic.
            /// </summary>
            if (Items.CanUseItem(2033))
            {
                if (!Bools.IsHealthPotRunning() && GameObjects.Player.HealthPercent < Managers.MinHealthPercent)
                {
                    Items.UseItem(2033);
                }
                else if (!Bools.IsManaPotRunning() && GameObjects.Player.ManaPercent < Managers.MinManaPercent)
                {
                    Items.UseItem(2033);
                }
            }
        }

        #endregion
    }
}