
#pragma warning disable 1587

namespace ExorAIO.Champions.Lucian
{
    using Utilities;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Weaving(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is Obj_AI_Hero) || Invulnerable.Check((Obj_AI_Hero)args.Target))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["mode"].GetValue<MenuList>().Index != 2)
            {
                if (!Game.CursorPos.IsUnderEnemyTurret()
                    || ((Obj_AI_Hero)args.Target).Health
                    < GameObjects.Player.GetAutoAttackDamage((Obj_AI_Hero)args.Target) * 2)
                {
                    switch (Vars.Menu["spells"]["e"]["mode"].GetValue<MenuList>().Index)
                    {
                        case 0:
                            Vars.E.Cast(
                                GameObjects.Player.ServerPosition.Extend(
                                    Game.CursorPos,
                                    GameObjects.Player.Distance(Game.CursorPos) < GameObjects.Player.GetRealAutoAttackRange()
                                        ? GameObjects.Player.BoundingRadius
                                        : 475f));
                            break;
                        case 1:
                            Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(Game.CursorPos, 475f));
                            break;
                    }

                    return;
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && ((Obj_AI_Hero)args.Target).IsValidTarget(Vars.Q.Range)
                && Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit((Obj_AI_Hero)args.Target);
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && ((Obj_AI_Hero)args.Target).IsValidTarget(Vars.W.Range)
                && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(Vars.W.GetPrediction((Obj_AI_Hero)args.Target).UnitPosition);
            }
        }

        #endregion
    }
}