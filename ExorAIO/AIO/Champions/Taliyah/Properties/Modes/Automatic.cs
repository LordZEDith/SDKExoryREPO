using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace ExorAIO.Champions.Taliyah
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
            ///     The AoE E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["aoe"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.CastIfWillHit(
                    Targets.Target, Vars.Menu["spells"]["e"]["aoe"].GetValue<MenuSliderButton>().SValue);
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            Bools.IsImmobile(t) && t.IsValidTarget(Vars.W.Range) &&
                            !Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    Vars.W.Cast(
                        target.ServerPosition,
                        target.IsFacing(GameObjects.Player) && GameObjects.Player.Distance(target) < Vars.AARange / 2
                            ? GameObjects.Player.ServerPosition.Extend(
                                target.ServerPosition, GameObjects.Player.Distance(target) * 2)
                            : GameObjects.Player.ServerPosition);
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            Bools.IsImmobile(t) && t.IsValidTarget(Vars.W.Range) &&
                            !Invulnerable.Check(t, DamageType.Magical)))
                {
                    Vars.E.Cast(target.ServerPosition);
                }
            }
        }
    }
}