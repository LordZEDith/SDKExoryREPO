using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Taliyah
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Taliyah.OnUpdate;
            GameObject.OnCreate += Taliyah.OnCreate;
            GameObject.OnDelete += Taliyah.OnDelete;
            Events.OnGapCloser += Taliyah.OnGapCloser;
            Events.OnInterruptableTarget += Taliyah.OnInterruptableTarget;
        }
    }
}
