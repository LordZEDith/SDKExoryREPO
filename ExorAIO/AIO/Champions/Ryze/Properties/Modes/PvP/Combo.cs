using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Ryze
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
            if (!Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target, DamageType.Magical))
            {
                return;
            }
            
            if (Bools.HasSheenBuff())
            {
                if (Targets.Target.IsValidTarget(Vars.AARange))
                {
                    return;
                }
            }

            /// <summary>
            ///     Dynamic Combo Logic.
            /// </summary>
            switch (Vars.RyzeStacks())
            {
                case 0:
                case 1:
                    if (Vars.RyzeStacks() == 0 ||
                        (GameObjects.Player.HealthPercent >
                            Vars.Menu["spells"]["q"]["shield"].GetValue<MenuSliderButton>().SValue) ||
                        !Vars.Menu["spells"]["q"]["shield"].GetValue<MenuSliderButton>().BValue)
                    {
                        /// <summary>
                        ///     The Q Combo Logic.
                        /// </summary>
                        if (Vars.Q.IsReady() &&
                            Targets.Target.IsValidTarget(Vars.Q.Range-50f) &&
                            Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
                        { 
                            Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
                        }
                    }

                    /// <summary>
                    ///     The W Combo Logic.
                    /// </summary>
                    if (Vars.W.IsReady() &&
                        Targets.Target.IsValidTarget(Vars.W.Range) &&
                        Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
                    {
                        Vars.W.CastOnUnit(Targets.Target);
                        return;
                    }

                    /// <summary>
                    ///     The E Combo Logic.
                    /// </summary>
                    if (Vars.E.IsReady() &&
                        Targets.Target.IsValidTarget(Vars.E.Range) &&
                        Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
                    {
                        Vars.E.CastOnUnit(Targets.Target);
                    }
                    break;

                default:
                    /// <summary>
                    ///     The Q Combo Logic.
                    /// </summary>
                    if (Vars.Q.IsReady() &&
                        Targets.Target.IsValidTarget(Vars.Q.Range-50f) &&
                        Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
                    { 
                        Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
                        return;
                    }
                    break;
            }
        }
    }
}