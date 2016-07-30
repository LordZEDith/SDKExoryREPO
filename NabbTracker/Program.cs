using LeagueSharp;
using LeagueSharp.SDK;

namespace NabbTracker
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
                Tracker.OnLoad();

                /// <summary>
                ///     Tells the player the assembly has been loaded.
                /// </summary>
                Game.PrintChat(
                    "[SDK]<b><font color='#228B22'>Nabb</font></b>Tracker: <font color='#228B22'>Ultima</font> - Loaded!");
            };
        }
    }
}