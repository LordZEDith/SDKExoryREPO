using LeagueSharp;
using LeagueSharp.SDK;

#pragma warning disable 1587

namespace AsunaCondemn
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
                if (!GameObjects.Player.ChampionName.Equals("Vayne"))
                {
                    Game.PrintChat(
                        "[SDK]<b><font color='#009aff'>Asuna</font></b>Condemn: <font color='#009aff'>Ultima</font> - Not Loaded: Vayne not Found.</font>");
                    return;
                }

                /// <summary>
                ///     Loads the assembly.
                /// </summary>
                Condem.OnLoad();

                /// <summary>
                ///     Tells the player the assembly has been loaded.
                /// </summary>
                Game.PrintChat(
                    "[SDK]<b><font color='#009aff'>Asuna</font></b>Condemn: <font color='#009aff'>Ultima</font> - Loaded!");
            };
        }
    }
}