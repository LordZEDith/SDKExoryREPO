
#pragma warning disable 1587

namespace ExorAIO.Champions.Diana
{
    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="GameObjectProcessSpellCastEventArgs" /> instance containing the event data.</param>
        public static void Weaving(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is Obj_AI_Hero) || Invulnerable.Check((Obj_AI_Hero)args.Target, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Weaving Logic.
            /// </summary>
            if (Vars.Q.IsReady() && ((Obj_AI_Hero)args.Target).IsValidTarget(Vars.Q.Range)
                && Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Vars.Q.GetPrediction((Obj_AI_Hero)args.Target).CastPosition);
                return;
            }

            /// <summary>
            ///     The R Combo Weaving Logic.
            /// </summary>
            if (Vars.R.IsReady() && ((Obj_AI_Hero)args.Target).HasBuff("dianamoonlight")
                && ((Obj_AI_Hero)args.Target).IsValidTarget(Vars.R.Range)
                && Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.R.CastOnUnit((Obj_AI_Hero)args.Target);
            }
        }

        #endregion
    }
}