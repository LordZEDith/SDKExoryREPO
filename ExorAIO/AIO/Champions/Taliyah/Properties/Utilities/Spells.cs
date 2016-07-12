using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Taliyah
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
            Vars.Q = new Spell(SpellSlot.Q, 1000f);
            Vars.W = new Spell(SpellSlot.W, 900f);
            Vars.E = new Spell(SpellSlot.E, 800f - GameObjects.Player.BoundingRadius*2);

            Vars.Q.SetSkillshot(0.275f, 100f, 3600f, false, SkillshotType.SkillshotLine);
            Vars.W.SetSkillshot(0.85f, 200f, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Vars.E.SetSkillshot(0.30f, 450f, float.MaxValue, false, SkillshotType.SkillshotCone);
        }
    }
}