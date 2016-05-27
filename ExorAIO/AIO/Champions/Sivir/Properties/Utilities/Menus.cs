using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

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
                    Vars.QMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.QMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.QMenu.Add(new MenuBool("logical",   "Logical",   true));
                    Vars.QMenu.Add(new MenuSliderButton("harass", "Harass / if Mana >= x%", 50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("clear",  "Clear / if Mana >= x%",  50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.WMenu.Add(new MenuSliderButton("clear",     "Clear / if Mana >= x%",     50, 0, 99, true));
                    Vars.WMenu.Add(new MenuSliderButton("buildings", "Buildings / if Mana >= x%", 50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
					Vars.EMenu.Add(new MenuSeparator("separator", "It has to be used in conjunction with Evade, else it will not shield Skillshots"));
					Vars.EMenu.Add(new MenuSeparator("separator2", "It it meant to shield what Evade doesn't support, like targetted spells, AoE, etc."));
                    Vars.EMenu.Add(new MenuBool("logical", "Logical", true));
					Vars.EMenu.Add(new MenuSlider("delay", "E Delay (ms)", 0, 0, 250));
					/*
					{
                        /// <summary>
                        ///     Sets the menu for the E Whitelist.
                        /// </summary>
                        Vars.WhiteListMenu = new Menu("whitelist", "Shield: Whitelist Menu", true);
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
								Vars.ListMenu = new Menu(target.ChampionName.ToLower(), $"{target.ChampionName}'s Spells", true);
								{
									foreach (var spell in target.Spells)
									{
										Vars.ListMenu.Add(
											new MenuBool(
												spell.Name.ToLower(),
												$"Shield: {spell.Name}",
												true));
									}
								}
                            }
                        }
                        Vars.EMenu.Add(Vars.WhiteListMenu);
                    }
					*/
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