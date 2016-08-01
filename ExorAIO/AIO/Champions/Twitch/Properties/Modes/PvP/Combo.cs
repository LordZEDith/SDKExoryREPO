using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

#pragma warning disable 1587

namespace ExorAIO.Champions.Twitch
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
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget() || Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range) && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>()
                                                                                                                   .Value)
            {
                if (!GameObjects.Player.HasBuff("TwitchFullAutomatic") && Targets.Target.GetBuffCount("twitchdeadlyvenom") <= 4)
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target)
                                    .CastPosition);
                }
            }
        }
    }
}
