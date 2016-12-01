
#pragma warning disable 1587

namespace ExorAIO.Champions.Kalista
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.Data.Enumerations;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;

    /// <summary>
    ///     The jungle HP bar offset.
    /// </summary>
    internal class JungleHpBarOffset
    {
        #region Fields

        internal string BaseSkinName;

        internal int Height;

        internal int Width;

        internal int XOffset;

        internal int YOffset;

        #endregion
    }

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class Healthbars
    {
        #region Static Fields

        /// <summary>
        ///     A list of the names of the champions who have a different healthbar type.
        /// </summary>
        public static readonly List<string> SpecialChampions = new List<string> { "Annie", "Jhin" };

        /// <summary>
        ///     The default enemy HP bar height offset.
        /// </summary>
        public static int SHeight = 8;

        /// <summary>
        ///     The default enemy HP bar width offset.
        /// </summary>
        public static int SWidth = 103;

        /// <summary>
        ///     The jungle HP bar offset list.
        /// </summary>
        internal static readonly List<JungleHpBarOffset> JungleHpBarOffsetList = new List<JungleHpBarOffset>
                                                                                     {
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Dragon_Air",
                                                                                                 Width = 140, Height = 4,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Dragon_Fire",
                                                                                                 Width = 140, Height = 4,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Dragon_Water",
                                                                                                 Width = 140, Height = 4,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Dragon_Earth",
                                                                                                 Width = 140, Height = 4,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Dragon_Elder",
                                                                                                 Width = 140, Height = 4,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Baron",
                                                                                                 Width = 190,
                                                                                                 Height = 10,
                                                                                                 XOffset = 16,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_RiftHerald",
                                                                                                 Width = 139, Height = 6,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 22
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName = "SRU_Red",
                                                                                                 Width = 139, Height = 4,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName = "SRU_Blue",
                                                                                                 Width = 139, Height = 4,
                                                                                                 XOffset = 12,
                                                                                                 YOffset = 24
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Gromp",
                                                                                                 Width = 86, Height = 2,
                                                                                                 XOffset = 1,
                                                                                                 YOffset = 7
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName = "Sru_Crab",
                                                                                                 Width = 61, Height = 2,
                                                                                                 XOffset = 1, YOffset = 5
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName = "SRU_Krug",
                                                                                                 Width = 79, Height = 2,
                                                                                                 XOffset = 1, YOffset = 7
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Razorbeak",
                                                                                                 Width = 74, Height = 2,
                                                                                                 XOffset = 1,
                                                                                                 YOffset = 7
                                                                                             },
                                                                                         new JungleHpBarOffset
                                                                                             {
                                                                                                 BaseSkinName =
                                                                                                     "SRU_Murkwolf",
                                                                                                 Width = 74, Height = 2,
                                                                                                 XOffset = 1,
                                                                                                 YOffset = 7
                                                                                             }
                                                                                     };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Loads the drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
                {
                    if (!Vars.E.IsReady() || !Vars.Menu["drawings"]["edmg"].GetValue<MenuBool>().Value)
                    {
                        return;
                    }

                    ObjectManager.Get<Obj_AI_Base>()
                        .Where(
                            h =>
                            h.IsValidTarget() && Kalista.IsPerfectRendTarget(h)
                            && (h is Obj_AI_Hero || Vars.JungleList.Contains(h.CharData.BaseSkinName)))
                        .ToList()
                        .ForEach(
                            unit =>
                                {
                                    /// <summary>
                                    ///     Defines what HPBar Offsets it should display.
                                    /// </summary>
                                    var mobOffset =
                                        JungleHpBarOffsetList.FirstOrDefault(
                                            x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName));

                                    var width = Vars.JungleList.Contains(unit.CharData.BaseSkinName)
                                                    ? mobOffset?.Width ?? SWidth
                                                    : SWidth;
                                    var height = Vars.JungleList.Contains(unit.CharData.BaseSkinName)
                                                     ? mobOffset?.Height ?? SHeight
                                                     : SHeight;
                                    var xOffset = Vars.JungleList.Contains(unit.CharData.BaseSkinName)
                                                      ? mobOffset?.XOffset ?? SxOffset((Obj_AI_Hero)unit)
                                                      : SxOffset((Obj_AI_Hero)unit);
                                    var yOffset = Vars.JungleList.Contains(unit.CharData.BaseSkinName)
                                                      ? mobOffset?.YOffset ?? SyOffset((Obj_AI_Hero)unit)
                                                      : SyOffset((Obj_AI_Hero)unit);
                                    var barPos = unit.HPBarPosition;
                                    {
                                        barPos.X += xOffset;
                                        barPos.Y += yOffset;
                                    }
                                    var drawEndXPos = barPos.X + width * (unit.HealthPercent / 100);
                                    var drawStartXPos = barPos.X
                                                        + (Vars.GetRealHealth(unit)
                                                           > (float)GameObjects.Player.GetSpellDamage(unit, SpellSlot.E)
                                                           + (float)
                                                             GameObjects.Player.GetSpellDamage(
                                                                 unit,
                                                                 SpellSlot.E,
                                                                 DamageStage.Buff)
                                                               ? width
                                                                 * ((Vars.GetRealHealth(unit)
                                                                     - ((float)
                                                                        GameObjects.Player.GetSpellDamage(
                                                                            unit,
                                                                            SpellSlot.E)
                                                                        + (float)
                                                                          GameObjects.Player.GetSpellDamage(
                                                                              unit,
                                                                              SpellSlot.E,
                                                                              DamageStage.Buff))) / unit.MaxHealth * 100
                                                                    / 100)
                                                               : 0);
                                    Drawing.DrawLine(
                                        drawStartXPos,
                                        barPos.Y,
                                        drawEndXPos,
                                        barPos.Y,
                                        height,
                                        Vars.GetRealHealth(unit)
                                        < (float)GameObjects.Player.GetSpellDamage(unit, SpellSlot.E)
                                        + (float)GameObjects.Player.GetSpellDamage(unit, SpellSlot.E, DamageStage.Buff)
                                            ? Color.Blue
                                            : Color.Orange);
                                    Drawing.DrawLine(
                                        drawStartXPos,
                                        barPos.Y,
                                        drawStartXPos,
                                        barPos.Y + height + 1,
                                        1,
                                        Color.Lime);
                                });
                };
        }

        /// <summary>
        ///     The default enemy HP bar x offset.
        /// </summary>
        public static int SxOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 1 : 10;
        }

        /// <summary>
        ///     The default enemy HP bar y offset.
        /// </summary>
        public static int SyOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 3 : 20;
        }

        #endregion
    }
}