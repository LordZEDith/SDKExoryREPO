using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Warwick
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
        public static void Combo(EventArgs args)
        {
            if ((Bools.HasSheenBuff() && Targets.Target.IsValidTarget(Vars.AARange)) ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && GameObjects.Player.IsWindingUp &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }

            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.Target);
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady())
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !t.IsUnderEnemyTurret() && t.IsValidTarget(Vars.R.Range) && !t.IsValidTarget(Vars.AARange) &&
                            Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value &&
                            Vars.Menu["spells"]["r"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                {
                    Vars.R.CastOnUnit(target);
                }
            }
        }
    }
}