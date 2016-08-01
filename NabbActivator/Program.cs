using LeagueSharp;
using LeagueSharp.SDK;

#pragma warning disable 1587

namespace NabbActivator
{
    /// <summary>
    ///     The application class.
    /// </summary>
    internal class Program
    {
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
                                 ///     Loads the activator index.
                                 /// </summary>
                                 Index.OnLoad();

                                 /// <summary>
                                 ///     Tells the player the assembly has been loaded.
                                 /// </summary>
                                 Game.PrintChat(
                                                "[SDK]<b><font color='#FF0000'>Nabb</font></b>Activator: <font color='#FF0000'>Ultima</font> - Loaded!");
                             };
        }
    }
}
