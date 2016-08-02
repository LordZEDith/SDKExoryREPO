using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

#pragma warning disable 1587

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
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["vision"].GetValue<MenuBool>().Value)
            {
                if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                    GameObjects.EnemyHeroes.Count(x => !x.IsDead && !x.IsVisible) >= 3)
                {
                    Vars.E.Cast();
                }
                else if (!NavMesh.IsWallOfGrass(GameObjects.Player.ServerPosition, 1))
                {
                    if (
                        GameObjects.EnemyHeroes.Any(
                            t =>
                                t.Distance(t.GetWaypoints().Last()) < 1500 &&
                                NavMesh.IsWallOfGrass((Vector3) t.GetWaypoints().Last(), 1) &&
                                GameObjects.Player.Distance(t.GetWaypoints().Last()) < Vars.W.Range))
                    {
                        Vars.E.Cast();
                    }
                }
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                GameObjects.Player.InFountain() &&
                Vars.R.Instance.Name.Equals("QuinnR"))
            {
                Vars.R.Cast();
            }
        }
    }
}
