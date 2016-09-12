namespace ExorAIO.Champions.Karthus
{
    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal class Spells
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.Q = new Spell(SpellSlot.Q, 875f);
            Vars.W = new Spell(SpellSlot.W, 1000f);
            Vars.E = new Spell(SpellSlot.E, 425f + GameObjects.Player.BoundingRadius);
            Vars.R = new Spell(SpellSlot.R);
            Vars.Q.SetSkillshot(0.5f, 100f, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Vars.W.SetSkillshot(
                0f,
                700f + 100 * GameObjects.Player.Spellbook.GetSpell(SpellSlot.W).Level,
                float.MaxValue,
                false,
                SkillshotType.SkillshotLine);
        }

        #endregion
    }
}