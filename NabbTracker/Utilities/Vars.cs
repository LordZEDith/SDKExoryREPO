using System.Collections.Generic;
using LeagueSharp;
using LeagueSharp.SDK.UI;
using SharpDX.Direct3D9;

namespace NabbTracker
{
    /// <summary>
    ///     The Vars class.
    /// </summary>
    internal class Vars
    {
        /// <summary>
        ///     Gets the SummonerSpell name.
        /// </summary>
        public static string GetSummonerSpellName;

        /// <summary>
        ///     Gets the spellslots.
        /// </summary>
        public static SpellSlot[] SpellSlots = {SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R};

        /// <summary>
        ///     Gets the summoner spellslots.
        /// </summary>
        public static SpellSlot[] SummonerSpellSlots = {SpellSlot.Summoner1, SpellSlot.Summoner2};

        /// <summary>
        ///     A list of the names of the champions who have a different healthbar.
        /// </summary>
        public static readonly List<string> SpecialChampions = new List<string> {"Annie", "Jhin"};

        /// <summary>
        ///     The Main Menu.
        /// </summary>
        public static Menu Menu { internal get; set; }

        /// <summary>
        ///     The Miscellaneous Menu.
        /// </summary>
        public static Menu MiscMenu { internal get; set; }

        /// <summary>
        ///     The Text fcnt.
        /// </summary>
        public static Font DisplayTextFont { internal get; set; } = new Font(Drawing.Direct3DDevice, new System.Drawing.Font("Tahoma", 8));

        /// <summary>
        ///     The Spells Healthbars X coordinate.
        /// </summary>
        public static int SpellX { internal get; set; }

        /// <summary>
        ///     The Spells Healthbars Y coordinate.
        /// </summary>
        public static int SpellY { internal get; set; }

        /// <summary>
        ///     The SpellLevel X coordinate.
        /// </summary>
        public static int SpellLevelX { internal get; set; }

        /// <summary>
        ///     The Healthbars Y coordinate.
        /// </summary>
        public static int SpellLevelY { internal get; set; }

        /// <summary>
        ///     The SummonerSpells Healthbar X coordinate.
        /// </summary>
        public static int SummonerSpellX { internal get; set; }

        /// <summary>
        ///     The SummonerSpells Healthbar Y coordinate.
        /// </summary>
        public static int SummonerSpellY { internal get; set; }

        /// <summary>
        ///     The Spells Healthbars X coordinate adjustment.
        /// </summary>
        public static int SpellXAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return target.IsMe
                    ? 34
                    : 17;
            }
            
            return target.IsMe
                ? 55
                : 10;
        }

        /// <summary>
        ///     The Spells Healthbars Y coordinate adjustment.
        /// </summary>
        public static int SpellYAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return 25;
            }
            
            return target.IsMe
                ? 25
                : 35;
        }

        /// <summary>
        ///     The Healthbars X coordinate adjustment.
        /// </summary>
        public static int SummonerSpellXAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return 2;
            }
            
            return 10;
        }

        /// <summary>
        ///     SummonerSpells The Healthbars Y coordinate adjustment.
        /// </summary>
        public static int SummonerSpellYAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return -12;
            }
            
            return target.IsMe
                ? -4
                : 4;
        }
    }
}