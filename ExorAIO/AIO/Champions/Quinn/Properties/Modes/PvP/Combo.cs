using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Quinn
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
            if (Bools.HasSheenBuff() ||
                Targets.Target.HasBuff("quinnw") ||
                Invulnerable.Check(Targets.Target) ||
                Targets.Target.IsValidTarget(Vars.AARange))
            {
                return;
            }

            /// <summary>
            ///     The Combo Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                !Vars.R.Instance.Name.Equals("QuinnRFinale") &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
                }
            }


            /// <summary>
            ///     The Combo E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(Targets.Target);
            }
        }
    }
}