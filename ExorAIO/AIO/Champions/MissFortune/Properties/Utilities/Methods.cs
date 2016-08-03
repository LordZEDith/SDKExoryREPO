namespace ExorAIO.Champions.MissFortune
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
        ///     Initializes the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += MissFortune.OnUpdate;
            Obj_AI_Base.OnDoCast += MissFortune.OnDoCast;
            Events.OnGapCloser += MissFortune.OnGapCloser;
            Variables.Orbwalker.OnAction += MissFortune.OnAction;
        }

        #endregion
    }
}