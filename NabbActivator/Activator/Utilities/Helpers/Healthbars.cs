
#pragma warning disable 1587

namespace NabbActivator
{
    using System.Drawing;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class Healthbars
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads the drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
                {
                    if (Vars.Smite.IsReady() && Vars.Smite.Slot != SpellSlot.Unknown)
                    {
                        if (!Vars.Menu["smite"]["drawings"]["damage"].GetValue<MenuBool>().Value)
                        {
                            return;
                        }

                        GameObjects.Jungle.Where(m => m.IsValidTarget() && !GameObjects.JungleSmall.Contains(m))
                            .ToList()
                            .ForEach(
                                unit =>
                                    {
                                        /// <summary>
                                        ///     Defines what HPBar Offsets it should display.
                                        /// </summary>
                                        var mobOffset =
                                            Vars.JungleHpBarOffsetList.FirstOrDefault(
                                                x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName));
                                        var barPos = unit.HPBarPosition;
                                        if (mobOffset != null)
                                        {
                                            barPos.X += mobOffset.XOffset;
                                            barPos.Y += mobOffset.YOffset;
                                            var drawStartXPos = barPos.X;
                                            var drawEndXPos = barPos.X
                                                              + mobOffset.Width
                                                              * (Vars.GetSmiteDamage / unit.MaxHealth * 100) / 100;
                                            Drawing.DrawLine(
                                                drawStartXPos,
                                                barPos.Y,
                                                drawEndXPos,
                                                barPos.Y,
                                                mobOffset.Height,
                                                unit.Health < Vars.GetSmiteDamage ? Color.Blue : Color.Orange);
                                            Drawing.DrawLine(
                                                drawEndXPos + 1,
                                                barPos.Y,
                                                drawEndXPos + 1,
                                                barPos.Y + mobOffset.Height + 1,
                                                2,
                                                Color.Lime);
                                        }
                                    });
                    }
                };
        }

        #endregion
    }
}