using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace ExorAIO.Champions.Graves
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
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t => Bools.IsImmobile(t) && !Invulnerable.Check(t) && t.IsValidTarget(Vars.Q.Range)))
                {
                    Vars.Q.Cast(target.ServerPosition);
                    return;
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            Bools.IsImmobile(t) && t.IsValidTarget(Vars.W.Range) &&
                            !Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    Vars.W.Cast(target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
            {
                if (
                    !GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t) && t.IsValidTarget(Vars.R.Range) &&
                            Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()]
                            .GetValue<MenuBool>().Value))
                {
                    return;
                }

                Vars.R.Cast(
                    Vars.R.GetPrediction(
                        GameObjects.EnemyHeroes.Where(
                            t =>
                                !Invulnerable.Check(t) && t.IsValidTarget(Vars.R.Range) &&
                                Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()]
                                .GetValue<MenuBool>().Value).OrderBy(o => o.Health).First()).UnitPosition);
            }
        }
    }
}
