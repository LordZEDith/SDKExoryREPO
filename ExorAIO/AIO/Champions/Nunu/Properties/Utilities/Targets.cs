namespace ExorAIO.Champions.Nunu
{
    using System.Collections.Generic;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The targets class.
    /// </summary>
    internal class Targets
    {
        #region Public Properties

        /// <summary>
        ///     The jungle minion targets.
        /// </summary>
        public static List<Obj_AI_Minion> JungleMinions
            =>
                GameObjects.Jungle.Where(
                    m =>
                    m.IsValidTarget(Vars.E.Range)
                    && (!GameObjects.JungleSmall.Contains(m) || m.CharData.BaseSkinName.Equals("Sru_Crab"))).ToList();

        /// <summary>
        ///     The minions target.
        /// </summary>
        public static List<Obj_AI_Minion> Minions
            => GameObjects.EnemyMinions.Where(m => m.IsMinion() && m.IsValidTarget(Vars.E.Range)).ToList();

        /// <summary>
        ///     The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(Vars.R.Range, DamageType.Magical);

        #endregion
    }
}