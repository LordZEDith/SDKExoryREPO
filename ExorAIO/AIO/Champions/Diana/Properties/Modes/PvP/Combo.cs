using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Diana
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
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() &&
                Targets.Target.IsValidTarget(Vars.AARange))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.W.Range)) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (Targets.Minions.Any(t => t.IsValidTarget(Vars.W.Range)) &&
                    Vars.Menu["miscellaneous"]["wcheck"].GetValue<MenuBool>().Value)
                {
                    return;
                }

                Vars.W.Cast();
            }

            if (!Targets.Target.IsValidTarget() ||
                Targets.Target.IsValidTarget(Vars.AARange) ||
                Invulnerable.Check(Targets.Target, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range))
            {
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            ///     The R Logics.
            /// </summary>
            if (Vars.R.IsReady())
            {
                /// <summary>
                ///     The R Combo Logic.
                /// </summary>
                if (Targets.Target.HasBuff("dianamoonlight") &&
                    Targets.Target.IsValidTarget(Vars.R.Range) &&
                    Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value &&
                    Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>().Value)
                {
                    if (!Targets.Target.IsUnderEnemyTurret() ||
                        !Vars.Menu["miscellaneous"]["safe"].GetValue<MenuBool>().Value)
                    {
                        Vars.R.CastOnUnit(Targets.Target);
                    }
                }

                /// <summary>
                ///     The R Gapclose Logic.
                /// </summary>
                else if (Targets.Target.IsValidTarget(Vars.R.Range*2) &&
                    Vars.Menu["miscellaneous"]["gapclose"].GetValue<MenuBool>().Value)
                {
                    foreach (var minion in Targets.Minions.Where(
                        m =>
                            m.IsValidTarget(Vars.R.Range) &&
                            m.Distance(Targets.Target) < Vars.Q.Range &&
                            Vars.GetRealHealth(m) >
                                (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                    {
                        Vars.Q.Cast(minion.ServerPosition);
                        DelayAction.Add(250, () => { Vars.R.CastOnUnit(minion); });
                    }
                }
            }
        }
    }
}