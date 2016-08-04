namespace AsunaCondemn
{
    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;

    /// <summary>
    ///     The settings class.
    /// </summary>
    internal class Spells
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.E = new Spell(SpellSlot.E, Vars.AaRange);
            Vars.Flash = new Spell(ObjectManager.Player.GetSpellSlot("SummonerFlash"), 425f);
            Vars.E.SetSkillshot(0.42f, 60f, 1200f, false, SkillshotType.SkillshotLine);
        }

        #endregion
    }
}