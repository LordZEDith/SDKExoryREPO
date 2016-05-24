using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Lux
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Lux.OnUpdate;
		    GameObject.OnCreate += Lux.OnCreate;
            GameObject.OnDelete += Lux.OnDelete;
            Events.OnGapCloser += Lux.OnGapCloser;
        }
    }
}