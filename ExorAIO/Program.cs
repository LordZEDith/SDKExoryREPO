using LeagueSharp.SDK;

#pragma warning disable 1587

namespace ExorAIO
{
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
                ///     Loads the AIO.
                /// </summary>
                AIO.OnLoad();
            };
        }
    }
}
