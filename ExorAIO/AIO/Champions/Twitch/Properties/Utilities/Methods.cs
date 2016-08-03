namespace ExorAIO.Champions.Twitch
{
    using LeagueSharp;
    using LeagueSharp.SDK;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Twitch.OnUpdate;
            Obj_AI_Base.OnDoCast += Twitch.OnDoCast;
            Spellbook.OnCastSpell += Twitch.OnCastSpell;
            Variables.Orbwalker.OnAction += Twitch.OnAction;
        }

        #endregion
    }
}