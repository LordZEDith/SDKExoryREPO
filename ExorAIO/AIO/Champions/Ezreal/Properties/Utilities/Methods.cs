namespace ExorAIO.Champions.Ezreal
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
            Game.OnUpdate += Ezreal.OnUpdate;
            Obj_AI_Base.OnDoCast += Ezreal.OnDoCast;
            Events.OnGapCloser += Ezreal.OnGapCloser;
            Obj_AI_Base.OnBuffAdd += Ezreal.OnBuffAdd;
        }

        #endregion
    }
}