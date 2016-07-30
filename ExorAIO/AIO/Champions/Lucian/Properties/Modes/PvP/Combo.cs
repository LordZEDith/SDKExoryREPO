using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.Lucian
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
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                !Targets.Target.IsValidTarget(Vars.AARange) &&
                Vars.Menu["spells"]["e"]["engager"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.Player.Distance(Game.CursorPos) > Vars.AARange &&
                    GameObjects.Player.ServerPosition.Extend(Game.CursorPos, 475f).CountEnemyHeroesInRange(1000f) < 3 &&
                    Targets.Target.Distance(GameObjects.Player.ServerPosition.Extend(Game.CursorPos, 475f)) <
                    Vars.AARange)
                {
                    Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(Game.CursorPos, 475f));
                }
            }

            if (!GameObjects.EnemyHeroes.Any(
                t =>
                    !Invulnerable.Check(t) &&
                    !t.IsValidTarget(Vars.Q.Range) &&
                    t.IsValidTarget(Vars.Q2.Range - 50f)))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["extended"]["excombo"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     Through enemy minions.
                /// </summary>
                foreach (var minion 
                    in from minion
                        in Targets.Minions.Where(m => m.IsValidTarget(Vars.Q.Range))
                        let polygon = new Geometry.Rectangle(
                            GameObjects.Player.ServerPosition,
                            GameObjects.Player.ServerPosition.Extend(minion.ServerPosition, Vars.Q2.Range - 50f),
                            Vars.Q2.Width)
                        where !polygon.IsOutside(
                            (Vector2) Vars.Q2.GetPrediction(GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range - 50f))).UnitPosition)
                        select minion)
                {
                    Vars.Q.CastOnUnit(minion);
                }
            }
        }
    }
}