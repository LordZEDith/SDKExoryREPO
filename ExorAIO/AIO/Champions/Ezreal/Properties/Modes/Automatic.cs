using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Ezreal
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
            ///     The Q LastHit Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["farmhelper"]) &&
                Vars.Menu["spells"]["q"]["farmhelper"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var minion in Targets.Minions.Where(
                    m =>
                        !m.IsValidTarget(Vars.AARange) &&
                        Vars.GetRealHealth(m) >
                            GameObjects.Player.GetAutoAttackDamage(m) &&
                        Vars.GetRealHealth(m) <
                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)).OrderBy(
                                o =>
                                    o.MaxHealth))
                {
                    if (!Vars.Q.GetPrediction(minion).CollisionObjects.Any())
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(minion).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The Tear Stacking Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                !Targets.Minions.Any() &&
                Bools.HasTear(GameObjects.Player) &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1500) == 0 &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["miscellaneous"]["tear"]) &&
                Vars.Menu["miscellaneous"]["tear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.Q.Cast(Game.CursorPos);
            }

            if (GameObjects.Player.TotalAttackDamage <
                GameObjects.Player.TotalMagicalDamage)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    if (!(Variables.Orbwalker.GetTarget() is Obj_AI_Hero))
                    {
                        return;
                    }
                    break;

                case OrbwalkingMode.LaneClear:
                    if (!(Variables.Orbwalker.GetTarget() is Obj_HQ) &&
                        !(Variables.Orbwalker.GetTarget() is Obj_AI_Turret) &&
                        !(Variables.Orbwalker.GetTarget() is Obj_BarracksDampener))
                    {
                        return;
                    }
                    break;

                default:
                    if (!GameObjects.Jungle.Contains(Variables.Orbwalker.GetTarget()) &&
                        !(Variables.Orbwalker.GetTarget() is Obj_HQ) &&
                        !(Variables.Orbwalker.GetTarget() is Obj_AI_Hero) &&
                        !(Variables.Orbwalker.GetTarget() is Obj_AI_Turret) &&
                        !(Variables.Orbwalker.GetTarget() is Obj_BarracksDampener))
                    {
                        return;
                    }
                    break;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                ObjectManager.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["logical"]) &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var target in GameObjects.AllyHeroes.Where(
                    t =>
                        !t.IsMe &&
                        t.IsWindingUp &&
                        t.IsValidTarget(Vars.W.Range, false)))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                }
            }
        }

        /// <summary>
        ///     Called while processing Spellcasting operations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="GameObjectProcessSpellCastEventArgs" /> instance containing the event data.</param>
        public static void AutoE(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe ||
                Invulnerable.Check(GameObjects.Player, DamageType.True, false))
            {
                return;
            }

            if (args.Target == null ||
                !sender.IsValidTarget())
            {
                return;
            }

            if (sender.IsEnemy &&
                sender is Obj_AI_Hero)
            {
                /// <summary>
                ///     Block Gangplank's Barrels.
                /// </summary>
                if ((sender as Obj_AI_Hero).ChampionName.Equals("Gangplank"))
                {
                    if (AutoAttack.IsAutoAttack(args.SData.Name) ||
                        args.SData.Name.Equals("GangplankQProceed"))
                    {
                        if ((args.Target as Obj_AI_Minion).Health == 1 &&
                            (args.Target as Obj_AI_Minion).CharData.BaseSkinName.Equals("gangplankbarrel"))
                        {
                            if (GameObjects.Player.Distance(args.Target) < 450)
                            {
                                Vars.E.Cast();
                            }
                        }
                    }
                    else if (args.SData.Name.Equals("GangplankEBarrelFuseMissile"))
                    {
                        if (GameObjects.Player.Distance(args.End) < 450)
                        {
                            Vars.E.Cast();
                        }
                    }
                }

                if (!args.Target.IsMe)
                {
                    return;
                }
                
                if (args.SData.Name.Contains("Summoner") ||
                    args.SData.Name.Equals("HextechGunblade") ||
                    args.SData.Name.Equals("BilgewaterCutlass") ||
                    args.SData.Name.Equals("ItemSwordOfFeastAndFamine"))
                {
                    return;
                }

                switch (args.SData.TargettingType)
                {
                    /// <summary>
                    ///     Special check for the AutoAttacks.
                    /// </summary>
                    case SpellDataTargetType.Unit:
                    case SpellDataTargetType.Self:
                    case SpellDataTargetType.LocationAoe:

                        if (args.SData.Name.Equals("NasusE") ||
                            args.SData.Name.Equals("GangplankE") ||
                            args.SData.Name.Equals("TrundleCircle") ||
                            args.SData.Name.Equals("TormentedSoil") ||
                            args.SData.Name.Equals("SwainDecrepify") ||
                            args.SData.Name.Equals("MissFortuneScattershot") ||
                            args.SData.Name.Equals("OrianaDissonanceCommand"))
                        {
                            return;
                        }

                        if (AutoAttack.IsAutoAttack(args.SData.Name))
                        {
                            if ((!sender.IsMelee && args.SData.Name.Contains("Card")) ||
                                sender.Buffs.Any(b => AutoAttack.IsAutoAttackReset(args.SData.Name)))
                            {
                                Vars.E.Cast();
                            }

                            return;
                        }

                        switch (sender.CharData.BaseSkinName)
                        {
                            case "Zed":
                                DelayAction.Add(200, ()=> { Vars.E.Cast(Game.CursorPos); });
                                break;

                            default:
                                Vars.E.Cast(Game.CursorPos);
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}