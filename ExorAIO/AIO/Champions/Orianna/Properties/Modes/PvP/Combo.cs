
#pragma warning disable 1587

namespace ExorAIO.Champions.Orianna
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

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

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && Targets.Target.Distance(Orianna.BallPosition) < Vars.W.Range
                && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                var polygon = new Geometry.Rectangle(
                    GameObjects.Player.ServerPosition,
                    GameObjects.Player.Position.Extend(
                        Orianna.BallPosition,
                        GameObjects.Player.Distance(Orianna.BallPosition)),
                    Vars.Q.Width);
                var objAiHero =
                    GameObjects.EnemyHeroes.FirstOrDefault(
                        t =>
                        t.IsValidTarget() && !Invulnerable.Check(t) && !polygon.IsOutside((Vector2)t.ServerPosition));
                if (objAiHero != null)
                {
                    Vars.E.CastOnUnit(GameObjects.Player);
                }
            }
        }

        #endregion
    }
}