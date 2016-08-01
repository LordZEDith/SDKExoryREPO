using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Nautilus
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
            Game.OnUpdate += Nautilus.OnUpdate;
            Obj_AI_Base.OnDoCast += Nautilus.OnDoCast;
            Variables.Orbwalker.OnAction += Nautilus.OnAction;
        }
    }
}
