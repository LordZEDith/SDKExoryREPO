namespace ExorAIO.Champions.Graves
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
        ///     Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Graves.OnUpdate;
            Obj_AI_Base.OnDoCast += Graves.OnDoCast;
            Obj_AI_Base.OnProcessSpellCast += Graves.OnProcessSpellCast;
            Events.OnGapCloser += Graves.OnGapCloser;
        }

        #endregion
    }
}