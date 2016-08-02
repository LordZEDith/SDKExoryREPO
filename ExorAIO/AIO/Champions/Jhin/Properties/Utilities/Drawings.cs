using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;
using Color = System.Drawing.Color;

#pragma warning disable 1587

namespace ExorAIO.Champions.Jhin
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
                                  ///     Loads the R Cone drawing.
                                  /// </summary>
                                  if (Vars.End != Vector3.Zero &&
                                      Vars.R.Instance.Name.Equals("JhinRShot") &&
                                      Vars.Menu["drawings"]["rc"].GetValue<MenuBool>().Value)
                                  {
                                      Vars.Cone.Draw(
                                          GameObjects.EnemyHeroes.Any(
                                              t => !Vars.Cone.IsOutside((Vector2) t.ServerPosition))
                                              ? Color.Green
                                              : Color.Red);
                                  }
                              };
        }
    }
}
