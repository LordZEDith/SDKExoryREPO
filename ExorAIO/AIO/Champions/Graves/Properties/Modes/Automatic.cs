
#pragma warning disable 1587

namespace ExorAIO.Champions.Graves
{
    using System;
    using System.Linq;

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
            ///     The Automatic R Orbwalking.
            /// </summary>
            if (Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value
                && Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
            {
                DelayAction.Add(
                    (int)(100 + Game.Ping / 2f),
                    () => { GameObjects.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos); });
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                        Bools.IsImmobile(t) && !Invulnerable.Check(t) && t.IsValidTarget(Vars.Q.Range)
                        && !Vars.AnyWall(GameObjects.Player.ServerPosition, t.ServerPosition)))
                {
                    Vars.Q.Cast(target.ServerPosition);
                    return;
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                        Bools.IsImmobile(t) && t.IsValidTarget(Vars.W.Range)
                        && !Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    Vars.W.Cast(target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Burst R Combo.
            /// </summary>
            var target2 =
                GameObjects.EnemyHeroes.Where(
                    t =>
                    !Invulnerable.Check(t) && t.IsValidTarget(Vars.E.Range + Vars.R.Range)
                    && Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>()
                           .Value).OrderBy(o => o.Health).FirstOrDefault();

            if (Vars.E.IsReady() && target2 != null && Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value
                && Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
            {
                Vars.E.Cast(target2.ServerPosition);
            }
        }

        #endregion
    }
}