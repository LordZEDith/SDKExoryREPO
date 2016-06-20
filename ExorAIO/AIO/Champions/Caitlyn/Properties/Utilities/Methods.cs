using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Caitlyn
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
            Game.OnUpdate += Caitlyn.OnUpdate;
            Obj_AI_Base.OnDoCast += Caitlyn.OnDoCast;
            Events.OnGapCloser += Caitlyn.OnGapCloser;
            Events.OnInterruptableTarget += Caitlyn.OnInterruptableTarget;
        }
    }
}