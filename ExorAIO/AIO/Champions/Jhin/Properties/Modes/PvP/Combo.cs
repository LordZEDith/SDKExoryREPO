using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

namespace ExorAIO.Champions.Jhin
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
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.R.Instance.Name.Equals("JhinRShot") &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyHeroes.Any(
                    t =>
                        t.IsValidTarget(Vars.R.Range) &&
                        !Vars.Cone.IsOutside((Vector2)t.ServerPosition)))
                {
                    foreach (var target in GameObjects.EnemyHeroes.Where(
                        t =>
                            t.IsValidTarget(Vars.R.Range) &&
                            !Vars.Cone.IsOutside((Vector2)t.ServerPosition)))
                    {
                        if (Vars.Menu["spells"]["r"]["nearmouse"].GetValue<MenuBool>().Value)
                        {
                            Vars.R.Cast(Vars.R.GetPrediction(GameObjects.EnemyHeroes.Where(
                                t =>
                                    !Vars.Cone.IsOutside((Vector2)t.ServerPosition) &&
                                    t.IsValidTarget(Vars.R.Range)).OrderBy(
                                        o =>
                                            o.Distance(Game.CursorPos)).First()).UnitPosition);
                            return;
                        }

                        Vars.R.Cast(Vars.R.GetPrediction(target).UnitPosition);
                        return;
                    }
                }

                Vars.R.Cast(Game.CursorPos);
            }

            if (Bools.HasSheenBuff() ||
				!Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target) ||
                Vars.R.Instance.Name.Equals("JhinRShot"))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                GameObjects.Player.HasBuff("JhinPassiveReload") &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.Target);
            }
        }
    }
}