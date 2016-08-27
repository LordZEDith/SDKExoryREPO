
#pragma warning disable 1587

namespace ExorAIO.Champions.Taliyah
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
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() && Targets.Target.IsValidTarget(Vars.AaRange) || !Targets.Target.IsValidTarget()
                || Invulnerable.Check(Targets.Target, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The E->W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range)
                && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value
                && (Vars.E.IsReady() || !Vars.Menu["spells"]["w"]["combofull"].GetValue<MenuBool>().Value))
            {
                switch (
                    Vars.Menu["spells"]["w"]["selection"][Targets.Target.ChampionName.ToLower()].GetValue<MenuList>()
                        .Index)
                {
                    case 0:
                        Vars.W.Cast(
                            Vars.W.GetPrediction(Targets.Target).CastPosition,
                            GameObjects.Player.ServerPosition);
                        break;
                    case 1:
                        Vars.W.Cast(
                            Vars.W.GetPrediction(Targets.Target).CastPosition,
                            GameObjects.Player.ServerPosition.Extend(
                                Targets.Target.ServerPosition,
                                GameObjects.Player.Distance(Targets.Target) * 2));
                        break;
                    default:
                        Vars.W.Cast(
                            Vars.W.GetPrediction(Targets.Target).CastPosition,
                            Vars.GetRealHealth(Targets.Target)
                            < (float)GameObjects.Player.GetSpellDamage(Targets.Target, SpellSlot.Q)
                            * (Taliyah.TerrainObject != null ? 1 : 3)
                            + (float)GameObjects.Player.GetSpellDamage(Targets.Target, SpellSlot.W)
                            + (float)GameObjects.Player.GetSpellDamage(Targets.Target, SpellSlot.E)
                                ? GameObjects.Player.ServerPosition
                                : GameObjects.Player.ServerPosition.Extend(
                                    Targets.Target.ServerPosition,
                                    GameObjects.Player.Distance(Targets.Target) * 2));
                        break;
                }
            }

            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range)
                && Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Targets.Target.ServerPosition);
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && !Vars.E.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range - 50f)
                && Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                if (Taliyah.TerrainObject != null
                    && Vars.Menu["spells"]["q"]["combofull"].GetValue<MenuBool>().Value)
                {
                    return;
                }

                if (!Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }

        #endregion
    }
}