using System.Linq;
using System.Windows.Forms;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using Menu = LeagueSharp.SDK.UI.Menu;

namespace NabbActivator
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menus.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the main menu.
            /// </summary>
            Vars.Menu = new Menu("activator", "NabbActivator", true);
            {
                /// <summary>
                ///     Sets the smite menu.
                /// </summary>
                Vars.SmiteMenu = new Menu("smite", "Smite Menu");
                {
                    /// <summary>
                    ///     Sets the smite options menu.
                    /// </summary>
                    Vars.SmiteMiscMenu = new Menu("misc", "Miscellaneous");
                    {
                        Vars.SmiteMiscMenu.Add(new MenuBool("combo",     "Combo",                                true));
                        Vars.SmiteMiscMenu.Add(new MenuBool("killsteal", "KillSteal",                            true));
                        Vars.SmiteMiscMenu.Add(new MenuBool("stacks",    "Keep 1 Stack for Dragon/Baron/Herald", true));
                        Vars.SmiteMiscMenu.Add(new MenuBool("limit",     "Only on Dragon/Baron/Herald"));
                    }
                    Vars.SmiteMenu.Add(Vars.SmiteMiscMenu);

                    /// <summary>
                    ///     Sets the smite whitelist menu.
                    /// </summary>
                    Vars.SmiteWhiteListMenu = new Menu("whitelist", "Whitelist");
                    {
                        foreach (var m in GameObjects.Jungle.Where(m => !GameObjects.JungleSmall.Contains(m)))
                        {
                            Vars.SmiteWhiteListMenu.Add(
                                new MenuBool(
                                    m.CharData.BaseSkinName.ToLower(),
                                    $"Use against: {m.CharData.BaseSkinName}",
                                true)
                            );
                        }
                    }
                    Vars.SmiteMenu.Add(Vars.SmiteWhiteListMenu);

                    /// <summary>
                    ///     Sets the drawings menu.
                    /// </summary>
                    Vars.DrawingsMenu = new Menu("drawings", "Drawings");
                    {
                        Vars.DrawingsMenu.Add(new MenuBool("range",  "Smite Range"));
                        Vars.DrawingsMenu.Add(new MenuBool("damage", "Smite Damage"));
                    }
                    Vars.SmiteMenu.Add(Vars.DrawingsMenu);
                }
                Vars.Menu.Add(Vars.SmiteMenu);

                /// <summary>
                ///     Sets the consumable sliders menu.
                /// </summary>
                Vars.SliderMenu = new Menu("consumables", "Consumables Menu");
                {
                    Vars.SliderMenu.Add(new MenuSlider("health", "Consumables: Health < x%", 50, 0, 100));
                    Vars.SliderMenu.Add(new MenuSlider("mana",   "Consumables: Mana < x%",   50, 0, 100));
                }
                Vars.Menu.Add(Vars.SliderMenu);

                /// <summary>
                ///     Sets the keys menu.
                /// </summary>
                Vars.KeysMenu = new Menu("keys", "Keybinds Menu");
                {
                    Vars.KeysMenu.Add(new MenuSeparator("separator", "The following will only work if Enabled."));
                    Vars.KeysMenu.Add(new MenuKeyBind("combo",     "Combo:",          Keys.Space, KeyBindType.Press));
                    Vars.KeysMenu.Add(new MenuKeyBind("laneclear", "LaneClear:",      Keys.V,     KeyBindType.Press));
                    Vars.KeysMenu.Add(new MenuKeyBind("smite",     "Smite (Toggle):", Keys.Y,     KeyBindType.Toggle));
                }
                Vars.Menu.Add(Vars.KeysMenu);

                Vars.Menu.Add(new MenuBool("offensives", "Offensives",           true));
                Vars.Menu.Add(new MenuBool("defensives", "Defensives",           true));
                Vars.Menu.Add(new MenuBool("spells",     "Spells",               true));
                Vars.Menu.Add(new MenuBool("cleansers",  "Cleansers",            true));
                Vars.Menu.Add(new MenuBool("potions",    "Potions",              true));
                Vars.Menu.Add(new MenuBool("resetters",  "Tiamat/Hydra/Titanic", true));
                Vars.Menu.Add(new MenuBool("randomizer", "Humanizer"));
            }
            Vars.Menu.Attach();
        }
    }
}