using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Diana
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
            if (!(args.Target is Obj_AI_Hero) || Invulnerable.Check(args.Target as Obj_AI_Hero, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Weaving Logic.
            /// </summary>
            if (Vars.Q.IsReady() && (args.Target as Obj_AI_Hero).IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Vars.Q.GetPrediction(args.Target as Obj_AI_Hero).CastPosition);
                return;
            }

            /// <summary>
            ///     The R Combo Weaving Logic.
            /// </summary>
            if (Vars.R.IsReady() && (args.Target as Obj_AI_Hero).HasBuff("dianamoonlight") &&
                (args.Target as Obj_AI_Hero).IsValidTarget(Vars.R.Range) &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.R.CastOnUnit(args.Target as Obj_AI_Hero);
            }
        }
    }
}