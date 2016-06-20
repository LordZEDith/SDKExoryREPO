using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

namespace ExorAIO.Champions.Vayne
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Vayne
    {
        /// <summary>
        ///     Loads Tryndamere.
        /// </summary>
        public void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();

            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            Drawings.Initialize();

            /// <summary>
            ///     Initializes the prediction drawings.
            /// </summary>
            PredictionDrawings.Initialize();
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (ObjectManager.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Updates the spells.
            /// </summary>
            Spells.Initialize();

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Logics.Automatic(args);
            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Logics.Killsteal(args);

            if (ObjectManager.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Hybrid:
                    Logics.Harass(args);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                AutoAttack.IsAutoAttack(args.SData.Name))
            {
                /// <summary>
                ///     Initializes the orbwalkingmodes.
                /// </summary>
                switch (Variables.Orbwalker.ActiveMode)
                {
                    case OrbwalkingMode.Combo:
                        Logics.Weaving(sender, args);
                        break;

                    case OrbwalkingMode.LastHit:
                        Logics.Clear(sender, args);
                        break;

                    case OrbwalkingMode.LaneClear:
                        Logics.Clear(sender, args);
                        Logics.JungleClear(sender, args);
                        Logics.BuildingClear(sender, args);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Vars.E.IsReady() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                /// <summary>
                ///     The Anti-GapCloser E Logic.
                /// </summary>
                if (args.Sender.IsMelee)
                {
                    if (args.IsDirectedToPlayer &&
                        Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
                    {
                        Vars.E.CastOnUnit(args.Sender);
                    }
                }

                /// <summary>
                ///     The Dash-Condemn Prediction Logic.
                /// </summary>
                else
                {
                    if (!GameObjects.Player.IsDashing() &&
                        GameObjects.Player.Distance(args.End) >
                            GameObjects.Player.BoundingRadius &&
                        Vars.Menu["spells"]["e"]["dashpred"].GetValue<MenuBool>().Value &&
                        Vars.Menu["spells"]["e"]["whitelist"][args.Sender.ChampionName.ToLower()].GetValue<MenuBool>().Value)
                    {
                        for (var i = 1; i < 10; i++)
                        {
                            if ((args.End + Vector3.Normalize(args.End - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&
                                (args.End + Vector3.Normalize(args.End - GameObjects.Player.ServerPosition) * i * 44).IsWall())
                            {
                                Console.WriteLine("DASHPREDICTION CONDEMN!!1!11");
                                Vars.E.CastOnUnit(args.Sender);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (Vars.E.IsReady() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["e"]["interrupter"].GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(args.Sender);
            }
        }
    }
}