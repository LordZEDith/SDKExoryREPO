
#pragma warning disable 1587

namespace ExorAIO.Champions.Orianna
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    using SharpDX;

    using Geometry = ExorAIO.Utilities.Geometry;

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
            ///     The Combo Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range)
                && Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
            }

            if (Orianna.BallPosition == null)
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady()
                && Targets.Target.Distance((Vector2)Orianna.BallPosition) < Vars.W.Range
                && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                foreach (
                    var ally in
                        GameObjects.AllyHeroes.OrderBy(o => o.Health).Where(t => t.IsValidTarget(Vars.E.Range, false)))
                {
                    var polygon = new Geometry.Rectangle(
                        ally.ServerPosition,
                        ally.ServerPosition.Extend(
                            (Vector2)Orianna.BallPosition,
                            ally.Distance((Vector2)Orianna.BallPosition)),
                        Vars.Q.Width);

                    var objAiHero =
                        GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                            t.IsValidTarget() && !Invulnerable.Check(t, DamageType.Magical)
                            && !polygon.IsOutside((Vector2)t.ServerPosition));
                    if (objAiHero != null)
                    {
                        Vars.E.CastOnUnit(ally);
                    }
                }
            }
        }

        #endregion
    }
}