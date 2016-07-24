using LeagueSharp;
using LeagueSharp.SDK;

namespace NabbAlerter
{
    /// <summary>
    ///     The application class.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     The entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
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
                Game.PrintChat("[SDK]<b><font color='#663096'>Nabb</font></b>Alerter: <font color='#663096'>Ultima</font> - Loaded!");
            };
        }
    }
}