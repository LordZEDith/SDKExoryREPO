using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Orianna
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
            Game.OnUpdate += Orianna.OnUpdate;
            Events.OnGapCloser += Orianna.OnGapCloser;
            Events.OnInterruptableTarget += Orianna.OnInterruptableTarget;
            Obj_AI_Base.OnProcessSpellCast += Orianna.OnProcessSpellCast;
        }
    }
}
