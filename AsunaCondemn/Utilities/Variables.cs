using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace AsunaCondemn
{
    /// <summary>
    ///     The Vars class.
    /// </summary>
    internal class Vars
    {
        /// <summary>
        ///     Gets or sets the E Spell.
        /// </summary>
        public static Spell E { internal get; set; }

        /// <summary>
        ///     Gets or sets the Flash Spell.
        /// </summary>
        public static Spell Flash { internal get; set; }

        /// <summary>
        ///     Gets or sets the assembly menu.
        /// </summary>
        public static Menu Menu { internal get; set; }

        /// <summary>
        ///     Gets or sets the E Spell menu.
        /// </summary>
        public static Menu EMenu { internal get; set; }

        /// <summary>
        ///     Gets or sets the WhiteList menu.
        /// </summary>
        public static Menu WhiteListMenu { internal get; set; }

        /// <summary>
        ///     Gets or sets the Drawings menu.
        /// </summary>
        public static Menu DrawingsMenu { internal get; set; }

        /// <summary>
        ///     Gets the Player's real AutoAttack-Range.
        /// </summary>
        public static float AARange => GameObjects.Player.GetRealAutoAttackRange();
    }
}
