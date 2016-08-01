using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

namespace NabbActivator
{
    /// <summary>
    ///     The targets class.
    /// </summary>
    internal class Targets
    {
        /// <summary>
        ///     The main enemy target.
        /// </summary>
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(1200f, DamageType.Magical);

        /// <summary>
        ///     The jungle minion targets.
        /// </summary>
        /// <summary>
        ///     The minions target.
        /// </summary>
        public static List<Obj_AI_Minion> Minions => GameObjects.AllyMinions.Where(m => m.IsMinion() && m.IsValidTarget(750f, false))
                                                                .ToList();

        /// <summary>
        ///     The jungle minion targets.
        /// </summary>
        public static List<Obj_AI_Minion> JungleMinions
            => GameObjects.Jungle.Where(m => m.IsValidTarget(500f) && !GameObjects.JungleSmall.Contains(m))
                          .ToList();
    }
}
