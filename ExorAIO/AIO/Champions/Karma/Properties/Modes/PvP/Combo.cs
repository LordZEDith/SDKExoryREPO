using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace ExorAIO.Champions.Karma
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
            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.AARange)) &&
                !GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.AARange)) && Vars.Menu["spells"]["e"]["engager"].GetValue<MenuBool>()
                                                                                                                       .Value)
            {
                Vars.E.CastOnUnit(GameObjects.Player);
            }
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range) && !Invulnerable.Check(Targets.Target, DamageType.Magical, false) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>()
                                                 .Value)
            {
                if (Vars.R.IsReady() && Vars.Menu["spells"]["w"]["lifesaver"].GetValue<MenuSliderButton>()
                                                                             .BValue
                    && Vars.Menu["spells"]["w"]["lifesaver"].GetValue<MenuSliderButton>()
                                                            .SValue > GameObjects.Player.HealthPercent)
                {
                    Vars.R.Cast();
                }
                Vars.W.CastOnUnit(Targets.Target);
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range - 100f) && !Invulnerable.Check(Targets.Target, DamageType.Magical) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>()
                                                 .Value)
            {
                if (!Vars.Q.GetPrediction(Targets.Target)
                         .CollisionObjects.Any(c => Targets.Minions.Contains(c)))
                {
                    if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["empq"].GetValue<MenuBool>()
                                                                            .Value)
                    {
                        Vars.R.Cast();
                    }
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target)
                                    .UnitPosition);
                }
            }
        }
    }
}
