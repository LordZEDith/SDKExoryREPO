using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

#pragma warning disable 1587

namespace ExorAIO.Champions.MissFortune
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
        public static void Clear(EventArgs args)
        {
            /// <summary>
            ///     The Q Extended Harass Logic.
            /// </summary>
            if (Vars.Q.IsReady())
            {
                /// <summary>
                ///     The Q Minion Harass Logic.
                /// </summary>
                if (GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["extended"]["exlaneclear"]) &&
                    Vars.Menu["spells"]["q"]["extended"]["exlaneclear"].GetValue<MenuSliderButton>().BValue)
                {
                    foreach (var minion 
                        in
                        from minion in
                            Targets.Minions.Where(
                                m =>
                                    m.IsValidTarget(Vars.Q.Range) &&
                                    (!Vars.Menu["spells"]["q"]["extended"]["exlaneclearkill"].GetValue<MenuBool>().Value ||
                                     m.Health < (float) GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                        let polygon =
                            new Geometry.Sector(
                                (Vector2) minion.ServerPosition,
                                (Vector2)
                                    minion.ServerPosition.Extend(
                                        GameObjects.Player.ServerPosition, -(Vars.Q2.Range - Vars.Q.Range)),
                                40f * (float) Math.PI / 180f, Vars.Q2.Range - Vars.Q.Range - 50f)
                        let target =
                            GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) && t.IsValidTarget(Vars.Q2.Range - 50f) &&
                                    ((Vars.PassiveTarget.IsValidTarget() && t.NetworkId == Vars.PassiveTarget.NetworkId) ||
                                     Targets.Minions.All(m => polygon.IsOutside((Vector2) m.ServerPosition))) &&
                                    Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>()
                                        .Value)
                        where target != null
                        where
                            !polygon.IsOutside((Vector2) target.ServerPosition) &&
                            !polygon.IsOutside(
                                (Vector2)
                                    Movement.GetPrediction(
                                        target, GameObjects.Player.Distance(target) / Vars.Q.Speed + Vars.Q.Delay)
                                        .UnitPosition)
                        select minion)
                    {
                        Vars.Q.CastOnUnit(minion);
                    }
                }
            }

            /// <summary>
            ///     The W Clear Logics.
            /// </summary>
            if (Vars.W.IsReady())
            {
                /// <summary>
                ///     The W JungleClear Logics.
                /// </summary>
                if (Targets.JungleMinions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) &&
                    GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["jungleclear"]) &&
                    Vars.Menu["spells"]["w"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.W.Cast();
                }

                /// <summary>
                ///     The W LaneClear Logics.
                /// </summary>
                else if (Targets.Minions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) &&
                         GameObjects.Player.ManaPercent >
                         ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["laneclear"]) &&
                         Vars.Menu["spells"]["w"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.W.Cast();
                }
            }

            /// <summary>
            ///     The E Clear Logics.
            /// </summary>
            if (Vars.E.IsReady())
            {
                /// <summary>
                ///     The E JungleClear Logics.
                /// </summary>
                if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                    Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.E.Cast(Targets.JungleMinions[0]);
                }

                /// <summary>
                ///     The E LaneClear Logics.
                /// </summary>
                else if (Targets.Minions.Any() &&
                         GameObjects.Player.ManaPercent >
                         ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                         Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue &&
                         Vars.E.GetCircularFarmLocation(Targets.Minions, Vars.E.Width).MinionsHit >= 4)
                {
                    Vars.E.Cast(Vars.E.GetCircularFarmLocation(Targets.Minions, Vars.E.Width).Position);
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void JungleClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            ///     The JungleClear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.JungleMinions.Any(m => m.IsValidTarget(Vars.Q.Range)) &&
                GameObjects.Player.ManaPercent >
                ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
                Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void BuildingClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(Variables.Orbwalker.GetTarget() is Obj_HQ) && !(Variables.Orbwalker.GetTarget() is Obj_AI_Turret) &&
                !(Variables.Orbwalker.GetTarget() is Obj_BarracksDampener))
            {
                return;
            }

            /// <summary>
            ///     The W BuildingClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent >
                ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["buildings"]) &&
                Vars.Menu["spells"]["w"]["buildings"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.W.Cast();
            }
        }
    }
}