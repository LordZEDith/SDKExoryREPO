namespace AsunaCondemn
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
            Game.OnUpdate += Condem.OnUpdate;
            Events.OnGapCloser += Condem.OnGapCloser;
        }

        #endregion
    }
}