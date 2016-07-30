using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Renekton
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
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                if (Targets.Minions.Any() &&
                    Targets.Minions.Count() >= 3)
                {
                    Vars.Q.Cast();
                }
                else if (Targets.JungleMinions.Any())
                {
                    if (!Vars.W.IsReady() &&
                        !GameObjects.Player.HasBuff("RenektonPreExecute"))
                    {
                        Vars.Q.Cast();
                    }
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
            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion == null)
            {
                return;
            }

            /// <summary>
            ///     The W JungleClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent >
                ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["manamanager"]) &&
                Vars.Menu["spells"]["w"]["jungleclear"].GetValue<MenuBool>().Value &&
                Targets.JungleMinions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion))
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
            if (!(Variables.Orbwalker.GetTarget() is Obj_HQ) &&
                !(Variables.Orbwalker.GetTarget() is Obj_AI_Turret) &&
                !(Variables.Orbwalker.GetTarget() is Obj_BarracksDampener))
            {
                return;
            }

            /// <summary>
            ///     The W BuildingClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent >
                ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["manamanager"]) &&
                Vars.Menu["spells"]["w"]["buildings"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }
        }
    }
}