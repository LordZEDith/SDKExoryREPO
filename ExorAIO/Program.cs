using LeagueSharp.SDK;

namespace ExorAIO
{
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
            LeagueSharp.SDK.Bootstrap.Init();

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