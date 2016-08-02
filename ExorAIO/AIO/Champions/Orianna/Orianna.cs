using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

#pragma warning disable 1587

namespace ExorAIO.Champions.Orianna
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Orianna
    {
        public static Vector3 BallPosition =
            ObjectManager.Get<Obj_AI_Minion>()
                         .FirstOrDefault(m => (int) m.Health == 1 && m.CharData.BaseSkinName.Equals("oriannaball"))?
                         .ServerPosition ?? (GameObjects.Player.HasBuff("orianaghostself")
                             ? GameObjects.Player.ServerPosition
                             : GameObjects.AllyHeroes.First(
                                 a => a.Buffs.Any(b => b.Caster.IsMe && b.Name.Equals("orianaghost")))
                                         ?.ServerPosition ?? Vector3.Zero);

        /// <summary>
        ///     Loads Orianna.
        /// </summary>
        public void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            Spells.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();

            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            Drawings.Initialize();
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Logics.Automatic(args);

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Logics.Killsteal(args);

            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Logics.Combo(args);
                    break;
                case OrbwalkingMode.Hybrid:
                    Logics.Harass(args);
                    break;
                case OrbwalkingMode.LaneClear:
                    Logics.Clear(args);
                    break;
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            /// <summary>
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (Vars.E.IsReady() &&
                args.Sender.IsMelee &&
                args.IsDirectedToPlayer &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(GameObjects.Player);
            }

            /// <summary>
            ///     The Anti-Gapcloser R.
            /// </summary>
            if (Vars.R.IsReady() &&
                BallPosition.Distance(args.End) < Vars.R.Width &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["r"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast();
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (Vars.R.IsReady() &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["r"]["interrupter"].GetValue<MenuBool>().Value)
            {
                if (Vars.Q.IsReady() &&
                    BallPosition.Distance(args.Sender.ServerPosition) > Vars.R.Range)
                {
                    Vars.Q.Cast(args.Sender.ServerPosition);
                }

                Vars.R.Cast();
            }
        }

        /// <summary>
        ///     Called when a <see cref="AttackableUnit" /> takes/gives damage.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AttackableUnitDamageEventArgs" /> instance containing the event data.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender is Obj_AI_Hero ||
                sender is Obj_AI_Turret)
            {
                if (sender.IsEnemy &&
                    args.Target != null &&
                    GameObjects.AllyHeroes.Any(a => a.NetworkId == args.Target.NetworkId))
                {
                    if (Vars.E.IsReady() &&
                        ((Obj_AI_Hero) args.Target).IsValidTarget(Vars.E.Range, false) &&
                        Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value &&
                        Vars.Menu["spells"]["e"]["whitelist"][((Obj_AI_Hero) args.Target).ChampionName.ToLower()]
                            .GetValue<MenuBool>().Value)
                    {
                        Vars.E.CastOnUnit((Obj_AI_Hero) args.Target);
                    }
                }
            }
        }

        /// <summary>
        ///     Called upon calling a spellcast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SpellbookCastSpellEventArgs" /> instance containing the event data.</param>
        public static void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (sender.Owner.IsMe &&
                args.Slot == SpellSlot.R &&
                Vars.Menu["spells"]["miscellanoeus"]["blockr"].GetValue<MenuBool>().Value)
            {
                var ballPosition =
                    ObjectManager.Get<Obj_AI_Minion>()
                                 .FirstOrDefault(o => o.CharData.BaseSkinName.Equals("oriannaball"))?.ServerPosition ??
                    GameObjects.Player.ServerPosition;
                if (!GameObjects.EnemyHeroes.Any(t => t.Distance(ballPosition) < Vars.R.Width))
                {
                    args.Process = false;
                }
            }
        }
    }
}
