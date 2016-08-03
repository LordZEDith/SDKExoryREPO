namespace ExorAIO.Champions.Olaf
{
    using LeagueSharp;

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
            Game.OnUpdate += Olaf.OnUpdate;
            Obj_AI_Base.OnDoCast += Olaf.OnDoCast;
        }

        #endregion
    }
}