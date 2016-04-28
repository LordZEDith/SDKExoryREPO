using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Pantheon
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
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",       true));
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuBool("harass",      "Harass",      true));
                    Vars.QMenu.Add(new MenuBool("jungleclear", "JungleClear", true));
                    Vars.QMenu.Add(
                        new MenuSlider("manamanager", "Harass/JungleClear: Mana >= x", 50, 10, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",       "Combo",                    true));
                    Vars.WMenu.Add(new MenuBool("killsteal",   "KillSteal",                true));
                    Vars.WMenu.Add(new MenuBool("interrupter", "Interrupt Enemy Channels", true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.EMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.EMenu.Add(
                        new MenuSlider("manamanager", "Clear: Mana >= x", 50, 10, 99));
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
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}