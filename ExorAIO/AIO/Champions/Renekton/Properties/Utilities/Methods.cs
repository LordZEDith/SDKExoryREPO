namespace ExorAIO.Champions.Renekton
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
            Game.OnUpdate += Renekton.OnUpdate;
            Obj_AI_Base.OnDoCast += Renekton.OnDoCast;
        }

        #endregion
    }
}