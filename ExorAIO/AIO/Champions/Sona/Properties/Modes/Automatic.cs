
#pragma warning disable 1587

namespace ExorAIO.Champions.Sona
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

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
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The Engager E Logic.
            /// </summary>
            if (Vars.E.IsReady()
                && GameObjects.Player.CountAllyHeroesInRange(1500f)
                >= Vars.Menu["spells"]["e"]["engager"].GetValue<MenuSliderButton>().SValue
                && Vars.Menu["spells"]["e"]["engager"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.Cast();
            }

            /// <summary>
            ///     The AoE R Logic.
            /// </summary>
            if (Vars.R.IsReady() && Targets.Target.IsValidTarget(Vars.R.Range)
                && Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.R.CastIfWillHit(
                    Targets.Target,
                    Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().SValue);
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value
                && Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
            {
                Vars.R.Cast(
                    Vars.R.GetPrediction(
                        GameObjects.EnemyHeroes.Where(
                            t =>
                            !Invulnerable.Check(t) && t.IsValidTarget(Vars.R.Range)
                            && Vars.Menu["spells"]["r"]["whitelist2"][Targets.Target.ChampionName.ToLower()]
                                   .GetValue<MenuBool>().Value).OrderBy(o => o.Health).First()).UnitPosition);
            }
        }

        #endregion
    }
}