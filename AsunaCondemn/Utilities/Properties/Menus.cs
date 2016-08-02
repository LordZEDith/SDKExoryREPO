using System.Windows.Forms;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using Menu = LeagueSharp.SDK.UI.Menu;

#pragma warning disable 1587

namespace AsunaCondemn
{
    /// <summary>
    ///     The settings class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the main menu.
            /// </summary>
            Vars.Menu = new Menu("asunacondemn", "AsunaCondemn", true);
            {
                Vars.Menu.Add(new MenuBool("enable", "Enable", true));
                Vars.Menu.Add(new MenuKeyBind("keybind", "Execute:", Keys.Space, KeyBindType.Press));

                /// <summary>
                ///     Sets the spells menu.
                /// </summary>
                Vars.EMenu = new Menu("features", "Features Menu:");
                {
                    Vars.EMenu.Add(new MenuBool("dashpred", "Enable Dash-Prediction", true));
                }
                Vars.Menu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the Whitelist.
                /// </summary>
                Vars.WhiteListMenu = new Menu("whitelist", "Whitelist Menu");
                {
                    foreach (var target in GameObjects.EnemyHeroes)
                    {
                        Vars.WhiteListMenu.Add(new MenuBool(target.ChampionName.ToLower(),
                                                            $"Use against: {target.ChampionName}",
                                                            true));
                    }
                }

                Vars.Menu.Add(Vars.WhiteListMenu);

                /// <summary>
                ///     Sets the drawings menu.
                /// </summary>
                Vars.DrawingsMenu = new Menu("drawings", "Drawings");
                {
                    Vars.DrawingsMenu.Add(new MenuBool("e", "E Prediction"));
                }
                Vars.Menu.Add(Vars.DrawingsMenu);
            }

            Vars.Menu.Attach();
        }
    }
}
