using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

#pragma warning disable 1587

namespace ExorAIO.Champions.Orianna
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            var ball =
                ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(o => o.CharData.BaseSkinName.Equals("oriannaball"));
            if (ball == null)
            {
                return;
            }

            /// <summary>
            ///     The R Automatic Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                GameObjects.EnemyHeroes.Count(t => t.Distance(ball) < Vars.R.Width) >=
                Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().SValue &&
                Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.R.Cast();
            }
        }
    }
}
