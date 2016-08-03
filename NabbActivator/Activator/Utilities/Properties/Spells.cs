namespace NabbActivator
{
    using LeagueSharp;
    using LeagueSharp.SDK;

    /// <summary>
    ///     The spells class.
    /// </summary>
    internal class ISpells
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            if (GameObjects.Player.ChampionName.Equals("GangPlank"))
            {
                Vars.W = new Spell(SpellSlot.W);
            }
            Vars.Smite = new Spell(SpellSlots.GetSmiteSlot(), 500f + GameObjects.Player.BoundingRadius);
        }

        #endregion
    }
}