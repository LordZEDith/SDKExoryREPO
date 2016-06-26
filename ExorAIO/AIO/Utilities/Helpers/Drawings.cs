using System.Linq;
using System.Drawing;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Color = System.Drawing.Color;

namespace ExorAIO.Utilities
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class Drawings
    {
        /// <summary>
        ///     Loads the drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
                /// <summary>
                ///     Loads the Q drawing,
                ///     Loads the Extended Q drawing.
                /// </summary>
                if (Vars.Q != null &&
                    Vars.Q.IsReady())
                {
                    if (Vars.Menu["drawings"]["q"] != null &&
                        Vars.Menu["drawings"]["q"].GetValue<MenuBool>().Value)
                    {
                        Render.Circle.DrawCircle(GameObjects.Player.Position, Vars.Q.Range, Color.Green, 1);
                    }

                    if (Vars.Menu["drawings"]["qe"] != null &&
                        Vars.Menu["drawings"]["qe"].GetValue<MenuBool>().Value)
                    {
                        Render.Circle.DrawCircle(GameObjects.Player.Position, Vars.Q2.Range, Color.LightGreen, 1);
                    }
                }

                /// <summary>
                ///     Loads the W drawing.
                /// </summary>
                if (Vars.W != null &&
                    Vars.W.IsReady() &&
                    Vars.Menu["drawings"]["w"] != null &&
                    Vars.Menu["drawings"]["w"].GetValue<MenuBool>().Value)
                {
                    Render.Circle.DrawCircle(GameObjects.Player.Position, Vars.W.Range, Color.Purple, 1);
                }

                /// <summary>
                ///     Loads the E drawing.
                /// </summary>
                if (Vars.E != null &&
                    Vars.E.IsReady() &&
                    Vars.Menu["drawings"]["e"] != null &&
                    Vars.Menu["drawings"]["e"].GetValue<MenuBool>().Value)
                {
                    Render.Circle.DrawCircle(GameObjects.Player.Position, Vars.E.Range, Color.Cyan, 1);
                }

                /// <summary>
                ///     Loads the R drawing.
                /// </summary>
                if (Vars.R != null &&
                    Vars.R.IsReady() &&
                    Vars.Menu["drawings"]["r"] != null &&
                    Vars.Menu["drawings"]["r"].GetValue<MenuBool>().Value)
                {
                    Render.Circle.DrawCircle(GameObjects.Player.Position, Vars.R.Range, Color.Red, 1);
                }
            };
        }
    }
}