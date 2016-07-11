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
                Vars.W.Cast();
            }

            if (!Targets.Target.IsValidTarget() ||
                Targets.Target.IsValidTarget(Vars.AARange) ||
                Invulnerable.Check(Targets.Target, DamageType.Magical, false))
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
                ///     The R Combo Logics.
                /// </summary>
                if (Targets.Target.IsValidTarget(Vars.R.Range) &&
                    Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
                {
                    /// <summary>
                    ///     The R Normal Combo Logic.
                    /// </summary>
                    if (Targets.Target.HasBuff("dianamoonlight") &&
                        Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>().Value)
                    {
                        if (!Targets.Target.IsUnderEnemyTurret() ||
                            !Vars.Menu["miscellaneous"]["safe"].GetValue<MenuBool>().Value)
                        {
                            Vars.R.CastOnUnit(Targets.Target);
                            return;
                        }
                    }

                    /// <summary>
                    ///     The Second R Combo Logic.
                    /// </summary>
                    if (!Vars.Q.IsReady() &&
                        !Targets.Target.HasBuff("dianamoonlight") &&
                        Vars.Menu["miscellaneous"]["rcombo"].GetValue<MenuBool>().Value)
                    {
                        DelayAction.Add(1000, () =>
                        {
                            if (!Vars.Q.IsReady() &&
                                !Targets.Target.HasBuff("dianamoonlight") &&
                                Vars.Menu["miscellaneous"]["rcombo"].GetValue<MenuBool>().Value)
                            {
                                Vars.R.CastOnUnit(Targets.Target);
                            }
                        });
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
                            m.Distance(Targets.Target) > Vars.Q.Range/2 &&
                            Vars.GetRealHealth(m) >
                                (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                    {
                        Vars.Q.Cast(minion.ServerPosition);
                        DelayAction.Add(250, () => { Vars.R.CastOnUnit(minion); });
                    }
                }
            }

            if (Vars.Q.IsReady() &&
                Vars.R.IsReady())
            {
                return;
            }
            
            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                !Targets.Target.IsValidTarget(Vars.AARange) &&
                Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast();
            }
        }
    }
}