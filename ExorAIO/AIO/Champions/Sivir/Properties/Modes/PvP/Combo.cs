using System;
using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;
using SharpDX;

namespace ExorAIO.Champions.Sivir
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() ||
                !Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Targets.Target.IsValidTarget(Vars.AARange) &&
                    Vars.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.High)
                {
                    Vars.Q.Cast(
                        Vars.Q.GetPrediction(Targets.Target)
                            .UnitPosition.Extend((Vector2)GameObjects.Player.ServerPosition, -140));
                }
            }
        }
    }
}