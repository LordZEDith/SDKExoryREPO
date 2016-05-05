using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ezreal
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
                    Vars.QMenu.Add(new MenuBool("combo",      "Combo",      true));
                    Vars.QMenu.Add(new MenuBool("killsteal",  "KillSteal",  true));
                    Vars.QMenu.Add(new MenuSliderButton("farmhelper", "FarmHelper / if Mana >= x%", 50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("harass",     "Harass / if Mana >= x%",     50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("clear",      "Clear / if Mana >= x%",      50, 0, 99, true));
                    {
                        /// <summary>
                        ///     Sets the menu for the Q Whitelist.
                        /// </summary>
                        Vars.WhiteListMenu = new Menu("whitelist", "Harass: Whitelist Menu");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        target.ChampionName.ToLower(),
                                        $"Use against: {target.ChampionName}",
                                        true));
                            }
                        }
                        Vars.QMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.WMenu.Add(new MenuSliderButton("logical", "Logical / if Mana >= x%", 50, 0, 99, true));
                    Vars.WMenu.Add(new MenuSliderButton("harass",  "Harass / if Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("antigrab", "Anti-Grab", true));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                Vars.RMenu = new Menu("r", "Use R to:");
                {
                    Vars.RMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.RMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    {
                        /// <summary>
                        ///     Sets the menu for the R Whitelist.
                        /// </summary>
                        Vars.WhiteList2Menu = new Menu("whitelist2", "Ultimate: Whitelist Menu");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteList2Menu.Add(
                                    new MenuBool(
                                        target.ChampionName.ToLower(),
                                        $"Use against: {target.ChampionName}",
                                        true));
                            }
                        }
                        Vars.RMenu.Add(Vars.WhiteList2Menu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            Vars.MiscMenu = new Menu("miscellaneous", "Miscellaneous");
            {
                Vars.MiscMenu.Add(new MenuSliderButton("tear", "Stack Tear / if Mana >= x%", 80, 1, 99, true));
            }
            Vars.Menu.Add(Vars.MiscMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}