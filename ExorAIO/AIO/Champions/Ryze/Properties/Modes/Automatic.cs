using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ryze
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
            ///     The Support Mode Option.
            /// </summary>
            if (Variables.Orbwalker.GetTarget() != null &&
                Variables.Orbwalker.GetTarget() is Obj_AI_Minion &&
                GameObjects.AllyHeroes.Any(a => a.Distance(GameObjects.Player) < 2500) &&
                Vars.Menu["miscellaneous"]["support"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.Hybrid &&
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.LaneClear);
            }

            /// <summary>
            ///     The No AA while in Combo option.
            /// </summary>
            if (Vars.Menu["miscellaneous"]["noaa"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    Bools.HasSheenBuff() ||
                    GameObjects.Player.ManaPercent < 10 ||
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo ||
                    (!Vars.Q.IsReady() && !Vars.W.IsReady() && !Vars.E.IsReady()));
            }

            /// <summary>
            ///     The Stacking Logics.
            /// </summary>
            if (Vars.Q.IsReady())
            {
                /// <summary>
                ///     The Tear Stacking Logic.
                /// </summary>
                if (!Targets.Minions.Any() &&
                    Bools.HasTear(GameObjects.Player) &&
                    GameObjects.Player.CountEnemyHeroesInRange(1500) == 0 &&
                    Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["miscellaneous"]["tear"]) &&
                    Vars.Menu["miscellaneous"]["tear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.Q.Cast(Game.CursorPos);
                }

                /// <summary>
                ///     The Passive Stacking Logic.
                /// </summary>
                if (!GameObjects.Player.HasBuff("RyzePassiveCharged") &&
                    Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                    Vars.Menu["miscellaneous"]["stacks"].GetValue<MenuSliderButton>().BValue &&
                    Vars.Menu["miscellaneous"]["stacks"].GetValue<MenuSliderButton>().SValue >
                        GameObjects.Player.GetBuffCount("RyzePassiveStack") &&
                    GameObjects.Player.ManaPercent >
                        Vars.Menu["miscellaneous"]["stacksmana"].GetValue<MenuSlider>().Value)
                {
                    Vars.Q.Cast(Game.CursorPos);
                }
            }
        }
    }
}