using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.KogMaw
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
        public static void Harass(EventArgs args)
        {
            /// <summary>
            ///     The Harass E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent >
                ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["clear"]) &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                if (GameObjects.EnemyHeroes.Any(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.E.Range)))
                {
                    if (Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).MinionsHit >= 3 &&
                        !new Geometry.Rectangle(
                            GameObjects.Player.ServerPosition,
                            GameObjects.Player.ServerPosition.Extend(Targets.Minions[0].ServerPosition, Vars.E.Range),
                            Vars.E.Width).IsOutside(
                                (Vector2) Vars.E.GetPrediction(GameObjects.EnemyHeroes.FirstOrDefault(
                                    t =>
                                        !Invulnerable.Check(t) &&
                                        t.IsValidTarget(Vars.E.Range))).UnitPosition))
                    {
                        Vars.E.Cast(Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).Position);
                    }
                }
            }
        }
    }
}