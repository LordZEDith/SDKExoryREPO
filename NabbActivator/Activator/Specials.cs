
#pragma warning disable 1587

namespace NabbActivator
{
    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;

    /// <summary>
    ///     The activator class.
    /// </summary>
    internal partial class Activator
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Specials(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Vars.Menu["defensives"].GetValue<MenuBool>().Value)
            {
                return;
            }

            if (sender != null && args.Target != null)
            {
                /// <summary>
                ///     The Ohmwrecker logic.
                /// </summary>
                if (Items.CanUseItem(3056) && sender.IsValidTarget(750f))
                {
                    if (args.Target.IsAlly && sender is Obj_AI_Turret && args.Target is Obj_AI_Hero)
                    {
                        Items.UseItem(3056, sender);
                    }
                }
            }
        }

        #endregion
    }
}