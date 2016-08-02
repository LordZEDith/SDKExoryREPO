using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace ExorAIO.Champions.Tristana
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
            if (Bools.HasSheenBuff() ||
                !(Variables.Orbwalker.GetTarget() as Obj_AI_Minion).IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.IsWindingUp &&
                (Targets.Minions.Any() || Targets.JungleMinions.Any()) &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }

            /// <summary>
            ///     The Clear E Logics.
            /// </summary>
            if (Vars.E.IsReady())
            {
                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                    Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.E.CastOnUnit(Variables.Orbwalker.GetTarget() as Obj_AI_Minion);
                }

                /// <summary>
                ///     The LaneClear E Logics.
                /// </summary>
                else
                {
                    /// <summary>
                    ///     The Aggressive LaneClear E Logic.
                    /// </summary>
                    if (GameObjects.EnemyHeroes.Any(t => !Invulnerable.Check(t) && t.IsValidTarget(Vars.W.Range)) &&
                        GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["harass"]) &&
                        Vars.Menu["spells"]["e"]["harass"].GetValue<MenuSliderButton>().BValue)
                    {
                        foreach (var minion in
                            Targets.Minions.Where(
                                m =>
                                    m.CountEnemyHeroesInRange(150f) > 0 &&
                                    Vars.GetRealHealth(m) < GameObjects.Player.GetAutoAttackDamage(m)))
                        {
                            Vars.E.CastOnUnit(minion);
                        }
                    }
                    else
                    {
                        /// <summary>
                        ///     The Normal LaneClear E Logic.
                        /// </summary>
                        if (Targets.Minions.Any() &&
                            GameObjects.Player.ManaPercent >
                            ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                            Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                        {
                            if (
                                Targets.Minions.Count(
                                    m => m.Distance(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) < 150f) >= 3)
                            {
                                Vars.E.CastOnUnit(Variables.Orbwalker.GetTarget() as Obj_AI_Minion);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void BuildingClear(EventArgs args)
        {
            if (!(Variables.Orbwalker.GetTarget() is Obj_AI_Turret))
            {
                return;
            }

            /// <summary>
            ///     The E BuildingClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent >
                ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["buildings"]) &&
                Vars.Menu["spells"]["e"]["buildings"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.CastOnUnit(Variables.Orbwalker.GetTarget() as Obj_AI_Turret);
            }
        }
    }
}
