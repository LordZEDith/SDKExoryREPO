using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

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
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Vars.Menu["enable"].GetValue<MenuSliderButton>().BValue)
            {
                return;
            }

            var objAiHero = sender as Obj_AI_Hero;
            if (objAiHero == null ||
                !objAiHero.IsValidTarget() ||
                !objAiHero.IsEnemy)
            {
                return;
            }
            if (!objAiHero.ServerPosition.IsOnScreen() &&
                Vars.Menu["onscreen"].GetValue<MenuBool>().Value)
            {
                return;
            }
            if (GameObjects.Player.Distance(objAiHero) > Vars.Menu["enable"].GetValue<MenuSliderButton>().SValue)
            {
                return;
            }

            /// <summary>
            ///     Check for the Not included Champions.
            /// </summary>
            if (Vars.NotIncludedChampions.Contains(objAiHero.ChampionName.ToLower()))
            {
                return;
            }

            switch (args.Slot)
            {
                case SpellSlot.R:

                    /// <summary>
                    ///     The Ultimate (R).
                    /// </summary>
                    if (Vars.Menu[objAiHero.ChampionName.ToLower()]["ultimate"].GetValue<MenuBool>().Value)
                    {
                        /// <summary>
                        ///     Exceptions check.
                        /// </summary>
                        if (Vars.ExChampions.Contains(objAiHero.ChampionName))
                        {
                            foreach (var s in Vars.RealSpells)
                            {
                                if (!objAiHero.Buffs.Any(b => b.Name.Equals(s)))
                                {
                                    return;
                                }
                                if (objAiHero.GetBuffCount(Vars.RealSpells.First(r => r.Equals(args.SData.Name))) > 1)
                                {
                                    return;
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
                                    Game.Say($"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())} no ulti");
                                    break;
                                case 2:
                                    Game.Say($"no ult {Vars.GetHumanName(objAiHero.ChampionName.ToLower())}");
                                    break;
                                case 3:
                                    Game.Say($"ult {Vars.GetHumanName(objAiHero.ChampionName.ToLower())}");
                                    break;
                                default:
                                    Game.Say($"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())} ult");
                                    break;
                            }
                        });
                    }

                    break;
                case SpellSlot.Summoner1:

                    /// <summary>
                    ///     The First SummonerSpell.
                    /// </summary>
                    if (Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[4].Name.ToLower()) != null &&
                        Vars.Menu[objAiHero.ChampionName.ToLower()]["sum1"].GetValue<MenuBool>().Value)
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
                                    Game.Say(
                                        $"no {Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[4].Name.ToLower())} " +
                                            $"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())}");
                                    break;
                                case 2:
                                    Game.Say($"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())} no " +
                                        $"{Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[4].Name.ToLower())}");
                                    break;
                                case 3:
                                    Game.Say(
                                        $"{Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[4].Name.ToLower())} " +
                                            $"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())}");
                                    break;
                                default:
                                    Game.Say($"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())} " +
                                        $"{Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[4].Name.ToLower())}");
                                    break;
                            }
                        });
                    }
                    break;
                case SpellSlot.Summoner2:

                    /// <summary>
                    ///     The Second SummonerSpell.
                    /// </summary>
                    if (Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[5].Name.ToLower()) != null &&
                        Vars.Menu[objAiHero.ChampionName.ToLower()]["sum2"].GetValue<MenuBool>().Value)
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
                                    Game.Say(
                                        $"no {Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[5].Name.ToLower())} " +
                                            $"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())}");
                                    break;
                                case 2:
                                    Game.Say($"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())} no " +
                                        $"{Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[5].Name.ToLower())}");
                                    break;
                                case 3:
                                    Game.Say(
                                        $"{Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[5].Name.ToLower())} " +
                                            $"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())}");
                                    break;
                                default:
                                    Game.Say($"{Vars.GetHumanName(objAiHero.ChampionName.ToLower())} " +
                                        $"{Vars.GetHumanSpellName(objAiHero.Spellbook.Spells[5].Name.ToLower())}");
                                    break;
                            }
                        });
                    }
                    break;
            }
        }
    }
}
