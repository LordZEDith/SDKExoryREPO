namespace ExorAIO.Champions.Nautilus
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
            Game.OnUpdate += Nautilus.OnUpdate;
            Obj_AI_Base.OnDoCast += Nautilus.OnDoCast;
            Variables.Orbwalker.OnAction += Nautilus.OnAction;
        }

        #endregion
    }
}