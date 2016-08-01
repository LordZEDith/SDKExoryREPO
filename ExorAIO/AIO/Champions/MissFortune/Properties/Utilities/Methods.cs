using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.MissFortune
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
            Game.OnUpdate += MissFortune.OnUpdate;
            Obj_AI_Base.OnDoCast += MissFortune.OnDoCast;
            Events.OnGapCloser += MissFortune.OnGapCloser;
            Variables.Orbwalker.OnAction += MissFortune.OnAction;
        }
    }
}
