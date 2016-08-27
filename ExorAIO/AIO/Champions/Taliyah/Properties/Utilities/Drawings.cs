
#pragma warning disable 1587

namespace ExorAIO.Champions.Taliyah
{
    using System;
    using System.Globalization;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    using SharpDX;

    /// <summary>
    ///     The prediction drawings class.
    /// </summary>
    internal class GroundDrawings
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads the ground drawings.
        /// </summary>
        public static void Initialize()
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            Drawing.OnDraw += delegate
                {
                    var workedGrounds = ObjectManager.Get<GameObject>()
                        .Where(o => o.Name.Equals("Taliyah_Base_Q_aoe_bright.troy"))
                        .ToList();
                    foreach (var obj in workedGrounds)
                    {
                        var text = new Render.Text(
                            (int)obj.Position.X,
                            (int)obj.Position.Y,
                            "t",
                            18,
                            new ColorBGRA(255, 255, 255, 255))
                                       {
                                           OutLined = true,
                                           PositionUpdate = () => Drawing.WorldToScreen(obj.Position),
                                           Centered = true
                                       };
                        var duration = 140 - (1.4 * GameObjects.Player.FlatCooldownMod);
                        text.VisibleCondition +=
                            sender =>
                            Vars.Menu["drawings"]["ground"].GetValue<MenuBool>().Value &&
                            Render.OnScreen(Drawing.WorldToScreen(obj.Position));

                        text.TextUpdate =
                            () =>
                            (duration - Environment.TickCount / 1000f).ToString(
                                CultureInfo.InvariantCulture);
                        text.Add();

                        if (duration - Environment.TickCount / 1000f <= 0 || !obj.IsValid)
                        {
                            text.Remove();
                        }
                    }
                };
        }

        #endregion
    }
}