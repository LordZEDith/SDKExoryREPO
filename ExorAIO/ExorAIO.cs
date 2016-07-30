using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO
{
    /// <summary>
    ///     The AIO class.
    /// </summary>
    internal class AIO
    {
        /// <summary>
        ///     Loads the Assembly's core processes.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Tries to load the current Champion.
            /// </summary>
            ExorAIO.Core.Bootstrap.LoadChampion();

            if (Vars.IsLoaded)
            {
                /// <summary>
                ///     Loads the Main Menu.
                /// </summary>
                Vars.Menu.Attach();
            }

            Game.PrintChat(
                $"[SDK]<b><font color='#009aff'>Exor</font></b>AIO: <font color='#009aff'>Ultima</font> - {GameObjects.Player.ChampionName} " + (Vars.IsLoaded
                    ? "Loaded." 
                    : "not supported."));
        }
    }
}