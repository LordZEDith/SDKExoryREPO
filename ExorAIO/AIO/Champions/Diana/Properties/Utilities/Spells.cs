using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Diana
{
    /// <summary>
    ///     The spell class.
    /// </summary>
    internal class Spells
    {
        /// <summary>
        ///     Initializes the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.Q = new Spell(SpellSlot.Q, 830f + GameObjects.Player.BoundingRadius);
            Vars.W = new Spell(SpellSlot.W, GameObjects.Player.GetRealAutoAttackRange());
            Vars.E = new Spell(SpellSlot.E, 350f + GameObjects.Player.BoundingRadius);
            Vars.R = new Spell(SpellSlot.R, 825f);

            Vars.Q.SetSkillshot(0.25f, 195f, 1400f, false, SkillshotType.SkillshotCircle);
        }
    }
}