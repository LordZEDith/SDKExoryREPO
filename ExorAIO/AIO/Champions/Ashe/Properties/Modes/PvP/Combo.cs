
#pragma warning disable 1587

namespace ExorAIO.Champions.Ashe
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget()
                || GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.AaRange + 50f)))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && !Invulnerable.Check(Targets.Target) && Targets.Target.IsValidTarget(Vars.W.Range)
                && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.W.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }

        #endregion
    }
}