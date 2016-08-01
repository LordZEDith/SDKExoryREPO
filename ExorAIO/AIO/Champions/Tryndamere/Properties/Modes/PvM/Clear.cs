using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

#pragma warning disable 1587

namespace ExorAIO.Champions.Tryndamere
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
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The LaneClear E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuBool>().Value)
            {
                if (Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).MinionsHit >= 4)
                {
                    Vars.E.Cast(Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).Position);
                }
            }
        }
    }
}