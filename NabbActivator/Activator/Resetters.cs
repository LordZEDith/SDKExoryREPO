using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace NabbActivator
{
    /// <summary>
    ///     The activator class.
    /// </summary>
    internal partial class Activator
    {
        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Resetters(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Vars.Menu["resetters"].GetValue<MenuBool>().Value)
            {
                return;
            }

            if (!Vars.Menu["combokey"].GetValue<MenuKeyBind>().Active ||
                !Vars.Menu["laneclearkey"].GetValue<MenuKeyBind>().Active)
            {
                return;
            }

            /// <summary>
            ///     If the player has no AA-Resets, triggers after normal AA, else after AA-Reset.
            /// </summary>
            if (sender.IsMe)
            {
                if ((!Vars.HasAnyReset && AutoAttack.IsAutoAttack(args.SData.Name)) ||
                    ObjectManager.Player.Buffs.Any(b => AutoAttack.IsAutoAttackReset(b.Name)))
                {
                    /// <summary>
                    ///     The Tiamat Melee Only logic.
                    /// </summary>
                    if (Items.CanUseItem(3077))
                    {
                        Items.UseItem(3077);
                    }

                    /// <summary>
                    ///     The Ravenous Hydra Melee Only logic.
                    /// </summary>
                    if (Items.CanUseItem(3074))
                    {
                        Items.UseItem(3074);
                    }

                    /// <summary>
                    ///     The Titanic Hydra Melee Only logic.
                    /// </summary>
                    if (Items.CanUseItem(3748))
                    {
                        Items.UseItem(3748);
                    }
                }
            }
        }
    }
}