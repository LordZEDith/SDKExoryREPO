using System.Collections.Generic;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Karma
{
    /// <summary>
    ///     The targets class.
    /// </summary>
    internal class Targets
    {
        /// <summary>
        ///     The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(Vars.Q.Range + 200f, DamageType.Magical);

        /// <summary>
        ///     The minions target.
        /// </summary>
        public static List<Obj_AI_Minion> Minions => GameObjects.EnemyMinions.Where(m => m.IsMinion() && m.IsValidTarget(Vars.Q.Range))
                                                                .ToList();

        /// <summary>
        ///     The jungle minion targets.
        /// </summary>
        public static List<Obj_AI_Minion> JungleMinions
            =>
                GameObjects.Jungle.Where(
                                         m =>
                                             m.IsValidTarget(Vars.Q.Range)
                                                 && (!GameObjects.JungleSmall.Contains(m) || m.CharData.BaseSkinName.Equals("Sru_Crab")))
                           .ToList();

        /// <summary>
        ///     The lowest ally in range.
        /// </summary>
        public static Obj_AI_Hero LowestAlly => GameObjects.AllyHeroes.Where(a => !a.IsMe && a.IsValidTarget(Vars.W.Range, false))
                                                           .OrderBy(o => o.Health)
                                                           .LastOrDefault();
    }
}
