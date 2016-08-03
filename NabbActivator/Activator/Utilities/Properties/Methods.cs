namespace NabbActivator
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
            Game.OnUpdate += Index.OnUpdate;
            Obj_AI_Base.OnDoCast += Index.OnDoCast;
        }

        #endregion
    }
}