
#pragma warning disable 1587

namespace ExorAIO.Champions.Jhin
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
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget() || Invulnerable.Check(Targets.Target)
                || Vars.R.Instance.Name.Equals("JhinRShot"))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range)
                && (GameObjects.Player.HasBuff("JhinPassiveReload")
                    || !Vars.Menu["spells"]["q"]["reloadcombo"].GetValue<MenuBool>().Value)
                && Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.Target);
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() && !GameObjects.Player.IsUnderEnemyTurret()
                && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                        !Invulnerable.Check(t) && t.HasBuff("jhinespotteddebuff")
                        && t.IsValidTarget(Vars.W.Range - 150f)
                        && Vars.Menu["spells"]["w"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                }
            }
        }

        #endregion
    }
}