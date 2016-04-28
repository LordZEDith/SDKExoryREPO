using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Jhin
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     The methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Jhin.OnUpdate;
            Obj_AI_Base.OnDoCast += Jhin.OnDoCast;
            Events.OnGapCloser += Jhin.OnGapCloser;
            Obj_AI_Base.OnProcessSpellCast += Jhin.OnProcessSpellCast;
        }
    }
}