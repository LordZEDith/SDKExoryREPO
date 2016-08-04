
#pragma warning disable 1587

namespace ExorAIO
{
    using LeagueSharp.SDK;

    internal class Program
    {
        #region Methods

        /// <summary>
        ///     The entry point of the application.
        /// </summary>
        private static void Main()
        {
            /// <summary>
            ///     Loads the Bootstrap.
            /// </summary>
            Bootstrap.Init();
            Events.OnLoad += (sender, eventArgs) =>
                {
                    /// <summary>
                    ///     Loads the AIO.
                    /// </summary>
                    Aio.OnLoad();
                };
        }

        #endregion
    }
}