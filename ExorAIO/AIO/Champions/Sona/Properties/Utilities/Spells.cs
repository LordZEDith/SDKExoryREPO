namespace ExorAIO.Champions.Sona
{
    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;

    /// <summary>
    ///     The spells class.
    /// </summary>
    internal class Spells
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.Q = new Spell(SpellSlot.Q, 850f);
            Vars.W = new Spell(SpellSlot.W, 850f); // check
            Vars.E = new Spell(SpellSlot.E, 1000f);
            Vars.R = new Spell(SpellSlot.R, 900f);
            Vars.R.SetSkillshot(0.25f, 140f, 2400f, false, SkillshotType.SkillshotLine);
        }

        #endregion
    }
}