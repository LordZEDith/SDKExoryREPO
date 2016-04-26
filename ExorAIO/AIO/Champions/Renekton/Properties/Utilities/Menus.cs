using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Renekton
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
                Vars.QMenu = new Menu("q", "Use Q to:");
                //.SetFontStyle(FontStyle.Regular, Color.Green);
                {
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",       true));
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("If 50+ Fury and there are enemies in Q range.", true);
                    Vars.QMenu.Add(new MenuBool("clear", "Clear", true));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Vars.WMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.WMenu.Add(new MenuBool("jungleclear", "JungleClear", true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    //.SetTooltip("It will only use the 1st E, you will have to manually use the 2nd E.", true);
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("lifesaver", "Lifesaver", true));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Green);

                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Purple);

                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}