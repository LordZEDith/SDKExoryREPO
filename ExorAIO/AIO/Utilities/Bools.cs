using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Utilities
{
    /// <summary>
    ///     The Bools class.
    /// </summary>
    internal class Bools
    {
        /// <summary>
        ///     Gets a value indicating whether the player has a sheen-like buff.
        /// </summary>
        public static bool HasSheenBuff()
            =>
                GameObjects.Player.HasBuff("sheen") ||
                GameObjects.Player.HasBuff("LichBane") ||
                GameObjects.Player.HasBuff("dianaarcready") ||
                GameObjects.Player.HasBuff("ItemFrozenFist");

        /// <summary>
        ///     Gets a value indicating whether a determined champion can move or not.
        /// </summary>
        public static bool IsImmobile(Obj_AI_Base target)
        {
            return
                target.MoveSpeed < 150 ||
                target.HasBuff("rebirth") ||
                target.HasBuff("chronorevive") ||
                target.HasBuff("lissandrarself") ||
                target.HasBuff("teleport_target") ||
                target.HasBuff("woogletswitchcap") ||
                target.HasBuff("zhonyasringshield") ||
                target.HasBuff("aatroxpassivedeath") ||
                IsValidStun(target as Obj_AI_Hero) ||
                IsValidSnare(target as Obj_AI_Hero) ||
                target.HasBuffOfType(BuffType.Flee) ||
                target.HasBuffOfType(BuffType.Taunt) ||
                target.HasBuffOfType(BuffType.Charm) ||
                target.HasBuffOfType(BuffType.Knockup) ||
                target.HasBuffOfType(BuffType.Suppression) ||
                (target as Obj_AI_Hero).IsCastingInterruptableSpell();
        }

        /// <summary>
        ///     Gets a value indicating whether the target has protection or not.
        /// </summary>
        /// <summary>
        ///     Gets a value indicating whether a determined root is worth cleansing.
        /// </summary>
        public static bool IsValidSnare(Obj_AI_Hero target)
        {
            return target.Buffs.Any(
                b =>
                    b.Type == BuffType.Snare &&
                    !Vars.InvalidSnareCasters.Contains((b.Caster as Obj_AI_Hero).ChampionName));
        }

        /// <summary>
        ///     Gets a value indicating whether a determined Stun is worth cleansing.
        /// </summary>
        public static bool IsValidStun(Obj_AI_Hero target)
        {
            return target.Buffs.Any(
                b =>
                    b.Type == BuffType.Stun &&
                    !Vars.InvalidStunCasters.Contains((b.Caster as Obj_AI_Hero).ChampionName));
        }

        /// <summary>
        ///     Gets a value indicating whether a determined champion has a stackable item.
        /// </summary>
        public static bool HasTear(Obj_AI_Hero target)
            =>
                target.InventoryItems.Any(
                    item =>
                        item.Id.Equals(ItemId.Tear_of_the_Goddess) ||
                        item.Id.Equals(ItemId.Archangels_Staff) ||
                        item.Id.Equals(ItemId.Manamune) ||
                        item.Id.Equals(ItemId.Tear_of_the_Goddess_Crystal_Scar) ||
                        item.Id.Equals(ItemId.Archangels_Staff_Crystal_Scar) ||
                        item.Id.Equals(ItemId.Manamune_Crystal_Scar));

        /// <summary>
        ///     Gets a value indicating whether BuffType is worth cleansing.
        /// </summary>
        public static bool ShouldCleanse(Obj_AI_Hero target)
            =>
                GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(1500f)) &&
                !Invulnerable.Check(GameObjects.Player, DamageType.True, false) &&
                (
                    target.HasBuffOfType(BuffType.Flee) ||
                    target.HasBuffOfType(BuffType.Charm) ||
                    target.HasBuffOfType(BuffType.Taunt) ||
                    target.HasBuffOfType(BuffType.Knockup) ||
                    target.HasBuffOfType(BuffType.Knockback) ||
                    target.HasBuffOfType(BuffType.Polymorph) ||
                    target.HasBuffOfType(BuffType.Suppression)
                    );

        /// <summary>
        ///     Defines whether the player has a deadly mark.
        /// </summary>
        public static bool HasDeadlyMark()
            =>
                !Invulnerable.Check(GameObjects.Player, DamageType.True, false) &&
                GameObjects.Player.HasBuff("zedrtargetmark") ||
                GameObjects.Player.HasBuff("summonerexhaust") ||
                GameObjects.Player.HasBuff("fizzmarinerdoombomb") ||
                GameObjects.Player.HasBuff("vladimirhemoplague") ||
                GameObjects.Player.HasBuff("mordekaiserchildrenofthegrave");

        /// <summary>
        ///     Returns true if the target is a perfectly valid rend target.
        /// </summary>
        public static bool IsPerfectRendTarget(Obj_AI_Base target)
        {
            if (target is Obj_AI_Minion)
            {
                if (target.IsValidTarget(Vars.E.Range) &&
                    target.HasBuff("kalistaexpungemarker"))
                {
                    return true;
                }
            }
            else if (target is Obj_AI_Hero)
            {
                if (target.IsValidTarget(Vars.E.Range) &&
                    target.HasBuff("kalistaexpungemarker") &&
                    !Invulnerable.Check(target as Obj_AI_Hero))
                {
                    return true;
                }
            }

            return false;
        }
    }
}