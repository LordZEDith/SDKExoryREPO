using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Taliyah
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Initializes the menus.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the menu for the spells.
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
                    Vars.QMenu.Add(new MenuSliderButton("harass",      "Harass / if Mana >= x%",      50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("laneclear",   "LaneClear / if Mana >= x%",   75, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("jungleclear", "JungleClear / if Mana >= x%", 50, 0, 99, true));
                    {
                        /// <summary>
                        ///     Sets the menu for the Q2.
                        /// </summary>
                        Vars.Q2Menu = new Menu("q2", "Threaded Volley Options:");
                        {
                            Vars.Q2Menu.Add(new MenuBool("combofull",       "Combo: Only with full Q.",       true));
                            Vars.Q2Menu.Add(new MenuBool("harassfull",      "Harass: Only with full Q."));
                            Vars.Q2Menu.Add(new MenuBool("laneclearfull",   "LaneClear: Only with full Q.",   true));
                            Vars.Q2Menu.Add(new MenuBool("jungleclearfull", "JungleClear: Only with full Q.", true));
                        }
                        Vars.QMenu.Add(Vars.Q2Menu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",       "Combo",                    true));
                    Vars.WMenu.Add(new MenuBool("logical",     "Logical",                  true));
                    Vars.WMenu.Add(new MenuBool("gapcloser",   "Anti-Gapcloser",           true));
                    Vars.WMenu.Add(new MenuBool("interrupter", "Interrupt Enemy Channels", true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo",       "Combo",                    true));
                    Vars.EMenu.Add(new MenuBool("logical",     "Logical",                  true));
                    Vars.EMenu.Add(new MenuBool("gapcloser",   "Anti-Gapcloser",           true));
                    Vars.EMenu.Add(new MenuBool("interrupter", "Interrupt Enemy Channels", true));
                    Vars.EMenu.Add(new MenuSliderButton("aoe",         "AoE / if enemies >= x",       3,  1,  5, true));
                    Vars.EMenu.Add(new MenuSliderButton("laneclear",   "LaneClear / if Mana >= x%",   50, 0, 99, true));
                    Vars.EMenu.Add(new MenuSliderButton("jungleclear", "JungleClear / if Mana >= x%", 50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the menu for the drawings.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}