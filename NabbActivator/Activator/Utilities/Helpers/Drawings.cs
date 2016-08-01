using System.Drawing;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace NabbActivator
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
                ///     Loads the Smite drawing.
                /// </summary>
                if (Vars.Smite.IsReady() && Vars.Smite.Slot != SpellSlot.Unknown &&
                    Vars.Menu["keys"]["smite"].GetValue<MenuKeyBind>().Active)
                {
                    if (Vars.Menu["smite"]["drawings"]["range"].GetValue<MenuBool>().Value)
                    {
                        Render.Circle.DrawCircle(GameObjects.Player.Position, Vars.Smite.Range, Color.Orange, 1);
                    }
                }
            };
        }
    }
}