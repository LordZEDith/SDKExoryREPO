using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Sivir
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
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    Vars.Q.Cast(target.ServerPosition);
                }
            }
        }

        /// <summary>
        ///     Called while processing Spellcasting operations.
        ///     Port this berbb :^)
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="GameObjectProcessSpellCastEventArgs" /> instance containing the event data.</param>
        public static void AutoShield(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe ||
                Invulnerable.Check(GameObjects.Player, DamageType.True, false))
            {
                return;
            }

            /// <summary>
            ///     Block Traps.
            /// </summary>
            if (ObjectManager.Get<Obj_AI_Minion>().Any(
                m =>
                    m.Distance(GameObjects.Player) < 175 &&
                    m.CharData.BaseSkinName.Equals("caitlyntrap")))
            {
                Vars.E.Cast();
                return;
            }

            if (args.Target == null ||
                !sender.IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     Block Dragon's AutoAttacks.
            /// </summary>
            if (args.Target.IsMe &&
                sender is Obj_AI_Minion)
            {
                if (sender.CharData.BaseSkinName.Equals("SRU_Baron") ||
                    sender.CharData.BaseSkinName.Contains("SRU_Dragon") ||
                    sender.CharData.BaseSkinName.Equals("SRU_RiftHerald"))
                {
                    Vars.E.Cast();
                }
            }
            else if (sender.IsEnemy &&
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
                    args.SData.Name.Equals("KatarinaE") ||
                    args.SData.Name.Equals("TalonCutthroat") ||
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
                                DelayAction.Add(200, ()=> { Vars.E.Cast(); });
                                break;

                            case "Caitlyn":
                            case "Nocturne":
                                DelayAction.Add(750, ()=> { Vars.E.Cast(); });
                                break;

                            default:
                                DelayAction.Add(Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, ()=> { Vars.E.Cast(); });
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
