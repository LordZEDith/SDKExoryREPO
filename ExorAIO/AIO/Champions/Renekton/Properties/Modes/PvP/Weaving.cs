
#pragma warning disable 1587

namespace ExorAIO.Champions.Renekton
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
        /// <param name="args">The args.</param>
        public static void Weaving(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is Obj_AI_Hero) || Invulnerable.Check((Obj_AI_Hero)args.Target))
            {
                return;
            }

            /// <summary>
            ///     The W Weaving Logic.
            /// </summary>
            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range)
                && !GameObjects.Player.HasBuff("RenektonPreExecute")
                && Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }
        }

        #endregion
    }
}