using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jhin
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Weaving(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is Obj_AI_Hero) || Bools.HasAnyImmunity(args.Target as Obj_AI_Hero))
            {
                return;
            }

            /// <summary>
            ///     The Q Weaving Logic.
            /// </summary>
            if (Vars.Q.IsReady() && (args.Target as Obj_AI_Hero).IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(args.Target as Obj_AI_Hero);
            }
        }
    }
}