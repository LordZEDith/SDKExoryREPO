using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

namespace NabbActivator
{
    /// <summary>
    ///     The main class.
    /// </summary>
    internal class Index
    {
        /// <summary>
        ///     Loads the Activator.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();
        }

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
			/*
            foreach (var target in GameObjects.EnemyHeroes)
            {
                foreach (var buff in target.Buffs.Where(b => b.Caster != null && b.Caster.IsMe))
                {
                    Console.WriteLine($"Enemy Champion: {target.ChampionName}, Buff: {buff.Name}");
                }
            }

            foreach (var buff in GameObjects.Player.Buffs.Where(b => b.Caster.IsMe))
            {
                if (buff.Name.Equals("caitlynheadshotcount") ||
                    buff.Name.Equals("caitlynheadshotpassive") ||
                    buff.Name.Equals("MasteryOnHitDamageStacker"))
                {
                    return;
                }

                Console.WriteLine($"My Champion: {GameObjects.Player.ChampionName}, Buff: {buff.Name}");
            }

            foreach (var target in GameObjects.AllyHeroes.Where(a => !a.IsMe))
            {
                foreach (var buff in target.Buffs.Where(b => b.Caster.IsMe))
                {
                    Console.WriteLine($"Ally Champion: {target.ChampionName}, Buff: {buff.Name}");
                }
            }

            foreach (var target in GameObjects.EnemyHeroes)
            {
                foreach (var buff in target.Buffs.Where(b => b.Caster.IsMe))
                {
                    Console.WriteLine($"Enemy Champion: {target.ChampionName}, Buff: {buff.Name}");
                }
            }
			*/
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!AutoAttack.IsAutoAttack(args.SData.Name))
            {
                Console.WriteLine($"OnProcessSpellCast: Name:{args.SData.Name}, Sender:{sender.CharData.BaseSkinName}");
            }
        }
    }
}