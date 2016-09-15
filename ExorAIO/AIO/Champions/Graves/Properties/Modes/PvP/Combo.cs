
#pragma warning disable 1587

namespace ExorAIO.Champions.Graves
{
    using System;

    using Utilities;

    using LeagueSharp;
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
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget() || Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range)
                && Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.Target.IsValidTarget(Vars.E.Range)
                && !Targets.Target.IsValidTarget(Vars.AaRange)
                && Vars.Menu["spells"]["e"]["engager"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.Player.Distance(Game.CursorPos) > Vars.AaRange
                    && GameObjects.Player.ServerPosition.Extend(Game.CursorPos, Vars.E.Range - Vars.AaRange)
                           .CountEnemyHeroesInRange(1000f) < 3
                    && Targets.Target.Distance(
                        GameObjects.Player.ServerPosition.Extend(Game.CursorPos, Vars.E.Range - Vars.AaRange))
                    < Vars.AaRange)
                {
                    Vars.E.Cast(Game.CursorPos);
                }
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() && !Vars.Q.IsReady() && Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.R.CastIfWillHit(Targets.Target, 2);
            }
        }

        #endregion
    }
}