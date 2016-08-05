namespace ExorAIO.Champions.Ashe
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
            Game.OnUpdate += Ashe.OnUpdate;
            Obj_AI_Base.OnDoCast += Ashe.OnDoCast;
            Events.OnGapCloser += Ashe.OnGapCloser;
            Events.OnInterruptableTarget += Ashe.OnInterruptableTarget;
        }

        #endregion
    }
}