namespace NabbAlerter
{
    using LeagueSharp;

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
            Obj_AI_Base.OnProcessSpellCast += Alerter.OnProcessSpellCast;
        }

        #endregion
    }
}