using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Lucian
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
            Game.OnUpdate += Lucian.OnUpdate;
            Obj_AI_Base.OnDoCast += Lucian.OnDoCast;
            Events.OnGapCloser += Lucian.OnGapCloser;
            Obj_AI_Base.OnPlayAnimation += Lucian.OnPlayAnimation;
            Variables.Orbwalker.OnAction += Lucian.OnAction;
        }
    }
}
