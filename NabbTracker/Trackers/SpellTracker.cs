using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;

namespace NabbTracker
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class SpellTracker
    {
        /// <summary>
        ///     Loads the range drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
                if (Vars.DisplayTextFont.IsDisposed || Drawing.Direct3DDevice.IsDisposed)
                {
                    return;
                }

                foreach (var unit in
                    GameObjects.Heroes.Where(
                        e =>
                            e.IsHPBarRendered &&
                            (e.IsMe && Vars.Menu["spelltracker"]["me"].GetValue<MenuBool>().Value ||
                             e.IsEnemy && Vars.Menu["spelltracker"]["enemies"].GetValue<MenuBool>().Value ||
                             e.IsAlly && !e.IsMe && Vars.Menu["spelltracker"]["allies"].GetValue<MenuBool>().Value)))
                {
                    for (var Spell = 0; Spell < Vars.SpellSlots.Count(); Spell++)
                    {
                        Vars.SpellX = (int) unit.HPBarPosition.X + Vars.SpellXAdjustment(unit) + Spell * 25;
                        Vars.SpellY = (int) unit.HPBarPosition.Y + Vars.SpellYAdjustment(unit);

                        Vars.DisplayTextFont.DrawText(
                            null,
                            unit.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time > 0
                                ? string.Format(
                                    "{0:0}",
                                    unit.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time + 1)
                                : Vars.SpellSlots[Spell].ToString(), Vars.SpellX, Vars.SpellY,
                            unit.Spellbook.GetSpell(Vars.SpellSlots[Spell]).Level < 1
                                ? Colors.Convert(Color.Gray)
                                : unit.Spellbook.GetSpell(Vars.SpellSlots[Spell])
                                    .SData.ManaCostArray.MaxOrDefault(value => value) > unit.Mana
                                    ? Colors.Convert(Color.Cyan)
                                    : unit.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time > 0 &&
                                      unit.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time <= 4
                                        ? Colors.Convert(Color.Yellow)
                                        : unit.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time >
                                          4
                                            ? Colors.Convert(Color.Red)
                                            : Colors.Convert(Color.LightGreen));

                        for (var level = 0; level <= unit.Spellbook.GetSpell(Vars.SpellSlots[Spell]).Level - 1; level++)
                        {
                            Vars.SpellLevelX = Vars.SpellX + level * 3 - 4;
                            Vars.SpellLevelY = Vars.SpellY + 4;

                            Vars.DisplayTextFont.DrawText(null, ".", Vars.SpellLevelX, Vars.SpellLevelY, Color.White);
                        }
                    }

                    for (var SummonerSpell = 0; SummonerSpell < Vars.SummonerSpellSlots.Count(); SummonerSpell++)
                    {
                        Vars.SummonerSpellX = (int) unit.HPBarPosition.X + Vars.SummonerSpellXAdjustment(unit) +
                                              SummonerSpell * 88;
                        Vars.SummonerSpellY = (int) unit.HPBarPosition.Y + Vars.SummonerSpellYAdjustment(unit);

                        switch (unit.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).Name.ToLower())
                        {
                            case "summonerflash":
                                Vars.GetSummonerSpellName = "Flash";
                                break;
                            case "summonerdot":
                                Vars.GetSummonerSpellName = "Ignite";
                                break;
                            case "summonerheal":
                                Vars.GetSummonerSpellName = "Heal";
                                break;
                            case "summonerteleport":
                                Vars.GetSummonerSpellName = "Teleport";
                                break;
                            case "summonerexhaust":
                                Vars.GetSummonerSpellName = "Exhaust";
                                break;
                            case "summonerhaste":
                                Vars.GetSummonerSpellName = "Ghost";
                                break;
                            case "summonerbarrier":
                                Vars.GetSummonerSpellName = "Barrier";
                                break;
                            case "summonerboost":
                                Vars.GetSummonerSpellName = "Cleanse";
                                break;
                            case "summonermana":
                                Vars.GetSummonerSpellName = "Clarity";
                                break;
                            case "summonerclairvoyance":
                                Vars.GetSummonerSpellName = "Clairvoyance";
                                break;
                            case "summonerodingarrison":
                                Vars.GetSummonerSpellName = "Garrison";
                                break;
                            case "summonersnowball":
                                Vars.GetSummonerSpellName = "Mark";
                                break;
                            default:
                                Vars.GetSummonerSpellName = "Smite";
                                break;
                        }

                        Vars.DisplayTextFont.DrawText(
                            null,
                            unit.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).CooldownExpires - Game.Time >
                            0
                                ? Vars.GetSummonerSpellName + ":" +
                                  string.Format(
                                      "{0:0}",
                                      unit.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).CooldownExpires -
                                      Game.Time + 1)
                                : Vars.GetSummonerSpellName + ": UP ", Vars.SummonerSpellX, Vars.SummonerSpellY,
                            unit.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).CooldownExpires - Game.Time >
                            0
                                ? Colors.Convert(Color.Red)
                                : Colors.Convert(Color.Yellow));
                    }
                }
            };
        }
    }
}