using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

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
        public static void Killsteal(EventArgs args)
        {
            /// <summary>
            ///     The KillSteal E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Invulnerable.Check(t) && t.IsValidTarget(Vars.E.Range) && !t.IsValidTarget(Vars.AARange) &&
                            Vars.GetRealHealth(t) <
                            GameObjects.Player.GetAutoAttackDamage(t) * 3 +
                            (float) GameObjects.Player.GetSpellDamage(t, SpellSlot.E)))
                {
                    Vars.E.Cast(Vars.E.GetPrediction(target).UnitPosition);
                }
            }
        }
    }
}