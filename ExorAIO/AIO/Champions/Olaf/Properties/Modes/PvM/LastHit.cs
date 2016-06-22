using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Olaf
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
        public static void LastHit(EventArgs args)
        {
            /// <summary>
            ///     The E LastHit Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["lasthit"].GetValue<MenuBool>().Value)
            {
                foreach (var minion in Targets.Minions.Where(
                    m =>
                        m.IsValidTarget(Vars.E.Range) &&
                        Vars.GetRealHealth(m) <
                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.E)))
                {
                    if (minion.GetMinionType() == MinionTypes.Siege ||
                        minion.GetMinionType() == MinionTypes.Super)
                    {
                        Vars.E.CastOnUnit(minion);
                    }
                }
            }
        }
    }
}