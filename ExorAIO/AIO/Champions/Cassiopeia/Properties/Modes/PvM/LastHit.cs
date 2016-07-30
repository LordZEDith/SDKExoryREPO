using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.Data.Enumerations;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Cassiopeia
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
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["lasthit"].GetValue<MenuSliderButton>().BValue)
            {
                DelayAction.Add(
                    Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, () =>
                    {
                        foreach (
                            var minion in
                                Targets.Minions.Where(
                                    m =>
                                        Vars.GetRealHealth(m) <
                                        (float) GameObjects.Player.GetSpellDamage(m, SpellSlot.E) +
                                        (m.HasBuffOfType(BuffType.Poison)
                                            ? (float)
                                                GameObjects.Player.GetSpellDamage(m, SpellSlot.E, DamageStage.Empowered)
                                            : 0)))
                        {
                            Vars.E.CastOnUnit(minion);
                        }
                    });
            }
        }
    }
}