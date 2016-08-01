using LeagueSharp;

namespace NabbAlerter
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
            Obj_AI_Base.OnProcessSpellCast += Alerter.OnProcessSpellCast;
        }
    }
}
