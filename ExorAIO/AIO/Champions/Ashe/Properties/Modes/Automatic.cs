
#pragma warning disable 1587

namespace ExorAIO.Champions.Ashe
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    using SharpDX;

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
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t => Bools.IsImmobile(t) && !Invulnerable.Check(t) && t.IsValidTarget(Vars.W.Range)))
                {
                    if (!Vars.W.GetPrediction(target).CollisionObjects.Any())
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady()
                && GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).Ammo
                >= (Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value ? 2 : 1)
                && Vars.Menu["spells"]["e"]["vision"].GetValue<MenuBool>().Value)
            {
                if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.None
                    && GameObjects.Player.CountEnemyHeroesInRange(1000f) == 0 && GameObjects.EnemyHeroes.Any()
                    && GameObjects.EnemyHeroes.Count(x => !x.IsDead && !x.IsVisible) >= 3)
                {
                    Vars.E.Cast(
                        Vars.Locations.Where(d => GameObjects.Player.Distance(d) > 1500f)
                            .OrderBy(d2 => GameObjects.Player.Distance(d2))
                            .FirstOrDefault());
                }
                else
                {
                    foreach (var target in
                        GameObjects.EnemyHeroes.Where(
                            t =>
                            t != null && t.Distance(t.GetWaypoints().Last()) < 1500
                            && NavMesh.IsWallOfGrass((Vector3)t.GetWaypoints().Last(), 1)))
                    {
                        Vars.E.Cast(target.GetWaypoints().Last());
                    }
                }
            }

            /// <summary>
            ///     The E -> R Combo Logics.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value
                && Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active
                && !Invulnerable.Check(Targets.Target, DamageType.Magical, false)
                && Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>()
                       .Value)
            {
                if (!Vars.R.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
                    {
                        Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).UnitPosition);
                    }
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).UnitPosition);
                }
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value
                && Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
            {
                if (
                    !GameObjects.EnemyHeroes.Any(
                        t =>
                        !Invulnerable.Check(t) && t.IsValidTarget(Vars.R.Range)
                        && Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()]
                               .GetValue<MenuBool>().Value))
                {
                    return;
                }

                Vars.R.Cast(
                    Vars.R.GetPrediction(
                        GameObjects.EnemyHeroes.Where(
                            t =>
                            !Invulnerable.Check(t) && t.IsValidTarget(Vars.R.Range)
                            && Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()]
                                   .GetValue<MenuBool>().Value).OrderBy(o => o.Health).First()).UnitPosition);
            }
        }

        #endregion
    }
}