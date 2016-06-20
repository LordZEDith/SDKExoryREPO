using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace NabbAlerter
{
    /// <summary>
    ///     The main class.
    /// </summary>
    internal class Alerter
    {
        /// <summary>
        ///     Called when the game loads itself.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();
        }

        /// <summary>
        ///     Handles the <see cref="E:ProcessSpell" /> event.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="Spell">The <see cref="GameObjectProcessSpellCastEventArgs" /> instance containing the event data.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Vars.Menu["enable"].GetValue<MenuSliderButton>().BValue)
            {
                return;
            }

            if ((sender as Obj_AI_Hero).IsValidTarget() ||
                !(sender as Obj_AI_Hero).IsEnemy)
            {
                return;
            }

            if (GameObjects.Player.Distance(sender as Obj_AI_Hero) > 
                Vars.Menu["enable"].GetValue<MenuSliderButton>().SValue)
            {
                return;
            }

            /// <summary>
            ///     Check for the Not included Champions.
            /// </summary>
            if (Vars.NotIncludedChampions.Contains((sender as Obj_AI_Hero).ChampionName.ToLower()))
            {
                return;
            }

            switch (args.Slot)
            {
                case SpellSlot.R:
                    /// <summary>
                    ///     The Ultimate (R).
                    /// </summary>
                    if (Vars.Menu[(sender as Obj_AI_Hero).ChampionName.ToLower()]["ultimate"].GetValue<MenuBool>().Value)
                    {
                        /// <summary>
                        ///     Exceptions check.
                        /// </summary>
                        if (Vars.ExChampions.Contains((sender as Obj_AI_Hero).ChampionName))
                        {
                            foreach (var s in Vars.RealSpells)
                            {
                                if (!(sender as Obj_AI_Hero).Buffs.Any(b => b.Name.Equals(s)))
                                {
                                    return;
                                }
                                else
                                {
                                    if ((sender as Obj_AI_Hero).GetBuffCount(Vars.RealSpells.First(r => r.Equals(args.SData.Name))) > 1)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                        /// <summary>
                        ///     Let's delay the alert by 5-10 seconds since we're not Sean Wrona.
                        /// </summary>
                        DelayAction.Add(WeightedRandom.Next(5000, 10000), () =>
                        {
                            if (Vars.Menu["nocombo"].GetValue<MenuBool>().Value &&
                                Vars.Menu["combokey"].GetValue<MenuKeyBind>().Active)
                            {
                                return;
                            }

                            /// <summary>
                            ///     Then we randomize the whole output (Structure/Names/SpellNames) to make it seem totally legit.
                            /// </summary>
                            switch (WeightedRandom.Next(1, 4))
                            {
                                case 1:
                                    Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} no ulti");
                                    break;

                                case 2:
                                    Game.Say($"no ult {Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                                    break;

                                case 3:
                                    Game.Say($"ult {Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                                    break;

                                default:
                                    Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} ult");
                                    break;
                            }
                        });
                    }
                    break;

                case SpellSlot.Summoner1:
                    /// <summary>
                    ///     The First SummonerSpell.
                    /// </summary>
                    if (Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower()) != null &&
                        Vars.Menu[(sender as Obj_AI_Hero).ChampionName.ToLower()]["sum1"].GetValue<MenuBool>().Value)
                    {
                        /// <summary>
                        ///     Let's delay the alert by 5-10 seconds since we're not Sean Wrona.
                        /// </summary>
                        DelayAction.Add(WeightedRandom.Next(5000, 10000), () =>
                        {
                            if (Vars.Menu["nocombo"].GetValue<MenuBool>().Value &&
                                Vars.Menu["combokey"].GetValue<MenuKeyBind>().Active)
                            {
                                return;
                            }

                            /// <summary>
                            ///     Then we randomize the whole output (Structure/Names/SpellNames) to make it seem totally legit.
                            /// </summary>
                            switch (WeightedRandom.Next(1, 4))
                            {
                                case 1:
                                    Game.Say($"no {Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())} " +
                                             $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                                    break;

                                case 2:
                                    Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} no " +
                                             $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())}");
                                    break;

                                case 3:
                                    Game.Say($"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())} " +
                                             $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                                    break;

                                default:
                                    Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} " +
                                             $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())}");
                                    break;
                            }
                        });
                    }
                    break;

                case SpellSlot.Summoner2:
                    /// <summary>
                    ///     The Second SummonerSpell.
                    /// </summary>
                    if (Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower()) != null &&
                        Vars.Menu[(sender as Obj_AI_Hero).ChampionName.ToLower()]["sum2"].GetValue<MenuBool>().Value)
                    {
                        /// <summary>
                        ///     Let's delay the alert by 5-10 seconds since we're not Sean Wrona.
                        /// </summary>
                        DelayAction.Add(WeightedRandom.Next(5000, 10000), () =>
                        {
                            if (Vars.Menu["nocombo"].GetValue<MenuBool>().Value &&
                                Vars.Menu["combokey"].GetValue<MenuKeyBind>().Active)
                            {
                                return;
                            }

                            /// <summary>
                            ///     Then we randomize the whole output (Structure/Names/SpellNames) to make it seem totally legit.
                            /// </summary>
                            switch (WeightedRandom.Next(1, 4))
                            {
                                case 1:
                                    Game.Say($"no {Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())} " +
                                             $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                                    break;

                                case 2:
                                    Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} no " +
                                             $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())}");
                                    break;

                                case 3:
                                    Game.Say($"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())} " +
                                             $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                                    break;

                                default:
                                    Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} " +
                                             $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())}");
                                    break;
                            }
                        });
                    }
                    break;

                default:
                    break;
            }
        }
    }
}