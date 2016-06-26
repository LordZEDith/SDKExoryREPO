using System;
using System.Linq;
using System.Drawing;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using ExorAIO.Utilities;
using SharpDX;
using Color = System.Drawing.Color;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.MissFortune
{
    /// <summary>
    ///     The prediction drawings class.
    /// </summary>
    internal class ConeDrawings
    {
        /// <summary>
        ///     Loads the range drawings.
        /// </summary>
        public static void Initialize()
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            Drawing.OnDraw += delegate
            {
                /// <summary>
                ///     Loads the Passive Target drawing.
                /// </summary>
                if (Vars.PassiveTarget.IsValidTarget() &&
                    Vars.Menu["drawings"]["p"].GetValue<MenuBool>().Value)
                {
                    Render.Circle.DrawCircle(Vars.PassiveTarget.Position, Vars.PassiveTarget.BoundingRadius, Color.LightGreen, 1);
                }

                /// <summary>
                ///     Loads the Q Cone drawings.
                /// </summary>
                if (Vars.Q.IsReady() &&
                    Vars.Menu["drawings"]["qc"].GetValue<MenuBool>().Value)
                {
                    foreach (var obj in ObjectManager.Get<Obj_AI_Base>().Where(m => m.IsValidTarget(Vars.Q.Range)))
                    {
                        var polygon = new Geometry.Sector(
                            (Vector2)obj.ServerPosition,
                            (Vector2)obj.ServerPosition.Extend(GameObjects.Player.ServerPosition,
                            -(Vars.Q2.Range - Vars.Q.Range)),
                            40f * (float)Math.PI / 180f,
                            (Vars.Q2.Range - Vars.Q.Range)-50f);

                        var target = GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                ((Vars.PassiveTarget.IsValidTarget() &&
                                    t.NetworkId == Vars.PassiveTarget.NetworkId) ||
                                    !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition))));

                        polygon.Draw(
                            !polygon.IsOutside((Vector2)target.ServerPosition) &&
                            !polygon.IsOutside(
                                (Vector2)Movement.GetPrediction(
                                    target,
                                    GameObjects.Player.Distance(target) / Vars.Q.Speed + Vars.Q.Delay).UnitPosition)

                            ? Color.Green
                            : Color.Red);
                    }
                }
            };
        }
    }
}