
#pragma warning disable 1587

namespace NabbAlerter
{
    using System.Linq;
    using System.Windows.Forms;

    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;
    using LeagueSharp.SDK.UI;

    using Menu = LeagueSharp.SDK.UI.Menu;

    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the main menu.
            /// </summary>
            Vars.Menu = new Menu("nabbalerter", "NabbAlerter", true);
            {
                Vars.Menu.Add(new MenuBool("nocombo", "Don't Alert while in Combo Mode", true));
                Vars.Menu.Add(new MenuBool("onscreen", "Don't Alert if sender isn't on screen", true));
                Vars.Menu.Add(new MenuKeyBind("combokey", "Combo:", Keys.Space, KeyBindType.Press));
                Vars.Menu.Add(new MenuSliderButton("enable", "Enable / Only if Range < x", 10000, 2000, 20000, true));

                /// <summary>
                ///     Checks the enemies.
                /// </summary>
                if (!GameObjects.EnemyHeroes.Any())
                {
                    Vars.Menu.Add(new MenuSeparator("none", "No Enemies Found!"));
                }
                else
                {
                    foreach (var target in GameObjects.EnemyHeroes)
                    {
                        /// <summary>
                        ///     Sets the enemies menus.
                        /// </summary>
                        Vars.HeroMenu = new Menu(target.ChampionName.ToLower(), target.ChampionName);
                        {
                            if (Vars.NotIncludedChampions.Contains(target.ChampionName.ToLower()))
                            {
                                Vars.HeroMenu.Add(
                                    new MenuSeparator(
                                        "notincluded",
                                        $"You don't need to alert about {target.ChampionName}'s Ultimate."));
                            }
                            else
                            {
                                Vars.HeroMenu.Add(new MenuBool("ultimate", "Alert R (Ultimate)", true));
                            }
                            Vars.HeroMenu.Add(new MenuBool("sum1", $"Alert {target.Spellbook.Spells[4].Name}", true));
                            Vars.HeroMenu.Add(new MenuBool("sum2", $"Alert {target.Spellbook.Spells[5].Name}", true));
                        }
                        Vars.Menu.Add(Vars.HeroMenu);
                    }
                }
            }

            Vars.Menu.Attach();
        }

        #endregion
    }
}