using System.Collections.Generic;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Jhin
{
    /// <summary>
    ///     The targets class.
    /// </summary>
    internal class Targets
    {
        /// <summary>
        ///     The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(Vars.R.Range, DamageType.Physical);

        /// <summary>
        ///     The R targets.
        /// </summary>
        public static List<Obj_AI_Hero> RTargets
            =>
                GameObjects.EnemyHeroes.Where(
                                              t =>
                                                  t.IsValidTarget(Vars.R.Range) && GameObjects.Player.IsFacing(t)
                                                      && !Invulnerable.Check(t, DamageType.True, false) &&
                                                      Vars.Menu["spells"]["r"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>()
                                                                                                                     .Value)
                           .ToList();

        /// <summary>
        ///     The minions target.
        /// </summary>
        public static List<Obj_AI_Minion> Minions => GameObjects.EnemyMinions.Where(m => m.IsMinion() && m.IsValidTarget(Vars.W.Range))
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
    }
}
