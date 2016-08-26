
#pragma warning disable 1587

namespace ExorAIO.Champions.Orianna
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;

    using SharpDX;

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
        public static void Automatic(EventArgs args)
        {
            var ball =
                ObjectManager.Get<Obj_AI_Minion>()
                    .FirstOrDefault(m => Math.Abs(m.Health) > 0 && m.CharData.BaseSkinName.Equals("oriannaball"));
            var ball3 =
                GameObjects.AllyHeroes.FirstOrDefault(
                    a => a.Buffs.Any(b => b.Caster.IsMe && b.Name.Equals("orianaghost")));

            Orianna.BallPosition = ball?.Position ?? (GameObjects.Player.HasBuff("orianaghostself")
                                                          ? GameObjects.Player.ServerPosition
                                                          : ball3?.Position);

            if (Orianna.BallPosition == null)
            {
                return;
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (Vars.R.IsReady()
                && GameObjects.EnemyHeroes.Count(
                    t =>
                    t.IsValidTarget()
                    && t.Distance((Vector2)Orianna.BallPosition) < Vars.R.Range - 25f)
                >= Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().SValue
                && Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.R.Cast();
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady())
            {
                foreach (var ally in GameObjects.AllyHeroes.Where(
                    a =>
                    a.IsValidTarget(Vars.E.Range, false) && a.CountEnemyHeroesInRange(Vars.R.Range)
                    >= Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().SValue
                    && Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value
                    && Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().BValue))
                {
                    Vars.E.CastOnUnit(ally);
                }
            }
        }

        #endregion
    }
}