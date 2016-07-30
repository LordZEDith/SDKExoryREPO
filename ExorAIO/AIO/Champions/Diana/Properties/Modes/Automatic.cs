using System;
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
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Misaya Orbwalking.
            /// </summary>
            if (Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
            {
                DelayAction.Add(
                    (int) (100 + Game.Ping / 2f),
                    () => { GameObjects.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos); });
            }

            /// <summary>
            ///     The Misaya Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Q.IsReady() &&
                GameObjects.Player.Mana > Vars.R.Instance.ManaCost + Vars.Q.Instance.ManaCost &&
                Targets.Target.IsValidTarget(Vars.R2.Range) &&
                Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active &&
                Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>().Value)
            {
                Vars.R.CastOnUnit(Targets.Target);
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
            }

            if (GameObjects.Player.IsDashing())
            {
                return;
            }

            /// <summary>
            ///     The AoE E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(Vars.E.Range) >=
                Vars.Menu["spells"]["e"]["aoe"].GetValue<MenuSliderButton>().SValue &&
                Vars.Menu["spells"]["e"]["aoe"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.Cast();
            }
        }
    }
}