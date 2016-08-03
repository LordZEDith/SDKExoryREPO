
#pragma warning disable 1587

namespace NabbAlerter
{
    using LeagueSharp;
    using LeagueSharp.SDK;

    /// <summary>
    ///     The application class.
    /// </summary>
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
                    ///     Loads the assembly.
                    /// </summary>
                    Alerter.OnLoad();

                    /// <summary>
                    ///     Tells the player the assembly has been loaded.
                    /// </summary>
                    Game.PrintChat(
                        "[SDK]<b><font color='#663096'>Nabb</font></b>Alerter: <font color='#663096'>Ultima</font> - Loaded!");
                };
        }

        #endregion
    }
}