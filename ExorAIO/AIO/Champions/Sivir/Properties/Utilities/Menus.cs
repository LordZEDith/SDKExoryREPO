using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.Data.Enumerations;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Sivir
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                /// <summary>
                ///     Sets the menu for the Q.
                /// </summary>
                Vars.QMenu = new Menu("q", "Use Q to:");
                {
                    Vars.QMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.QMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.QMenu.Add(new MenuBool("logical", "Logical", true));
                    Vars.QMenu.Add(new MenuSliderButton("harass", "Harass / if Mana >= x%", 50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("clear", "Clear / if Mana >= x%", 50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.WMenu.Add(new MenuSliderButton("clear", "Clear / if Mana >= x%", 50, 0, 99, true));
                    Vars.WMenu.Add(new MenuSliderButton("buildings", "Buildings / if Mana >= x%", 50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(
                        new MenuSeparator(
                            "separator",
                            "It has to be used in conjunction with Evade, else it will not shield Skillshots"));
                    Vars.EMenu.Add(
                        new MenuSeparator(
                            "separator2", "It is meant to shield what Evade doesn't support, like targetted spells."));
                    Vars.EMenu.Add(new MenuBool("logical", "Logical", true));
                    Vars.EMenu.Add(new MenuSlider("delay", "E Delay (ms)", 0, 0, 250));
                    {
                        /// <summary>
                        ///     Sets the menu for the E Whitelist.
                        /// </summary>
                        Vars.WhiteListMenu = new Menu("whitelist", "Shield: Whitelist Menu", true);
                        {
                            Vars.WhiteListMenu.Add(new MenuBool("minions", "Shield: Dragon/Baron Attacks", true));
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                if (enemy.ChampionName.Equals("Alistar"))
                                {
                                    Vars.WhiteListMenu.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.pulverize",
                                            $"Shield: {enemy.ChampionName}'s Q", true));
                                }

                                if (enemy.ChampionName.Equals("Braum"))
                                {
                                    Vars.WhiteListMenu.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.braumbasicattackpassiveoverride",
                                            $"Shield: {enemy.ChampionName}'s Passive", true));
                                }

                                if (enemy.ChampionName.Equals("Jax"))
                                {
                                    Vars.WhiteListMenu.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.jaxcounterstrike",
                                            $"Shield: {enemy.ChampionName}'s E", true));
                                }

                                if (enemy.ChampionName.Equals("KogMaw"))
                                {
                                    Vars.WhiteListMenu.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.kogmawicathiansurprise",
                                            $"Shield: {enemy.ChampionName}'s Passive", true));
                                }

                                if (enemy.ChampionName.Equals("Udyr"))
                                {
                                    Vars.WhiteListMenu.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.udyrbearattack",
                                            $"Shield: {enemy.ChampionName}'s E", true));
                                }

                                if (
                                    SpellDatabase.Get()
                                        .Where(
                                            s =>
                                                !s.SpellName.Equals("KatarinaE") &&
                                                !s.SpellName.Equals("TalonCutthroat") &&
                                                s.ChampionName.Equals(enemy.ChampionName)) == null)
                                {
                                    return;
                                }

                                foreach (var spell in
                                    SpellDatabase.Get()
                                        .Where(
                                            s =>
                                                !s.SpellName.Equals("KatarinaE") &&
                                                !s.SpellName.Equals("TalonCutthroat") &&
                                                s.ChampionName.Equals(enemy.ChampionName)))
                                {
                                    if ((enemy.IsMelee && spell.CastType.Contains(CastType.Activate) &&
                                         spell.SpellType.HasFlag(SpellType.Activated) &&
                                         AutoAttack.IsAutoAttackReset(spell.SpellName)) ||
                                        ((spell.SpellType.HasFlag(SpellType.Targeted) ||
                                          spell.SpellType.HasFlag(SpellType.TargetedMissile)) &&
                                         spell.CastType.Contains(CastType.EnemyChampions)))
                                    {
                                        Vars.WhiteListMenu.Add(
                                            new MenuBool(
                                                $"{enemy.ChampionName.ToLower()}.{spell.SpellName.ToLower()}",
                                                $"Shield: {enemy.ChampionName}'s {spell.Slot}", true));
                                    }
                                }
                            }
                        }
                        Vars.EMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.EMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}