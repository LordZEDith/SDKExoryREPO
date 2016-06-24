using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Karma
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The AoE E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["empe"].GetValue<MenuBool>().Value &&
                GameObjects.Player.CountEnemyHeroesInRange(2000f) >= 2 &&
                GameObjects.Player.CountAllyHeroesInRange(600f) >=
                    Vars.Menu["spells"]["e"]["aoe"].GetValue<MenuSliderButton>().SValue + 1 &&
                Vars.Menu["spells"]["e"]["aoe"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.R.Cast();
                Vars.E.CastOnUnit(GameObjects.Player);
            }
        }
    }
}