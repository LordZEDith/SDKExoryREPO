using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

#pragma warning disable 1587

namespace ExorAIO.Champions.Jax
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
            ///     The Clear E Logics.
            /// </summary>
            if (Vars.E.IsReady() && GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["clear"]) &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuSliderButton>()
                                                 .BValue)
            {
                /// <summary>
                ///     The LaneClear E Logic.
                /// </summary>
                if (Targets.Minions.Count >= 3 && GameObjects.Player.CountEnemyHeroesInRange(2000f) == 0)
                {
                    Vars.E.Cast();
                }

                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any(m => m.IsValidTarget(Vars.E.Range)))
                {
                    Vars.E.Cast();
                }
            }

            /// <summary>
            ///     The Q JungleGrab Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.JungleMinions.Any(m => !m.IsValidTarget(Vars.E.Range)) &&
                GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["junglegrab"]) &&
                Vars.Menu["spells"]["q"]["junglegrab"].GetValue<MenuSliderButton>()
                                                      .BValue)
            {
                Vars.Q.CastOnUnit(Targets.JungleMinions.FirstOrDefault(m => !m.IsValidTarget(Vars.E.Range)));
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Clear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(Variables.Orbwalker.GetTarget() is Obj_AI_Minion) || !Targets.Minions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion))
            {
                return;
            }

            /// <summary>
            ///     The Clear W Logics.
            /// </summary>
            if (Vars.W.IsReady() && GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>()
                                                 .BValue)
            {
                Vars.W.Cast();
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
            if (Vars.W.IsReady() && GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["buildings"]) &&
                Vars.Menu["spells"]["w"]["buildings"].GetValue<MenuSliderButton>()
                                                     .BValue)
            {
                Vars.W.Cast();
            }
        }
    }
}
