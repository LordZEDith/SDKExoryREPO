using System.Drawing;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace ExorAIO.Champions.Orianna
{
    /// <summary>
    ///     The prediction drawings class.
    /// </summary>
    internal class BallDrawings
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
                if (Vars.Menu["drawings"]["ball"].GetValue<MenuBool>().Value)
                {
                    Render.Circle.DrawCircle(Orianna.BallPosition, 100f, Color.Blue, 4);
                }
                if (Vars.W.IsReady() &&
                    Vars.Menu["drawings"]["ballw"].GetValue<MenuBool>().Value)
                {
                    Render.Circle.DrawCircle(Orianna.BallPosition, Vars.W.Range, Color.Cyan);
                }
                if (Vars.R.IsReady() &&
                    Vars.Menu["drawings"]["ballr"].GetValue<MenuBool>().Value)
                {
                    Render.Circle.DrawCircle(Orianna.BallPosition, Vars.R.Range, Color.Red);
                }
            };
        }
    }
}
