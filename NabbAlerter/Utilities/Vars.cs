namespace NabbAlerter
{
    using System.Collections.Generic;

    using LeagueSharp.SDK.UI;

    /// <summary>
    ///     The Vars class.
    /// </summary>
    internal class Vars
    {
        #region Static Fields

        /// <summary>
        ///     A list containing the names of the champs whose ultimate is useless to track.
        /// </summary>
        public static readonly List<string> ExChampions = new List<string>
                                                              {
                                                                  "Ahri", "Annie", "Irelia", "Jhin", "Riven", "Shaco",
                                                                  "Zed"
                                                              };

        /// <summary>
        ///     A list containing the names of the champs whose ultimate is useless to track.
        /// </summary>
        public static readonly List<string> NotIncludedChampions = new List<string>
                                                                       {
                                                                           "akali", "anivia", "swain", "jayce",
                                                                           "nidalee",
                                                                           "blitzcrank", "chogath", "corki", "darius",
                                                                           "diana", "elise", "karma", "kassadin",
                                                                           "khazix",
                                                                           "kogmaw", "leblanc", "maokai", "quinn",
                                                                           "ryze",
                                                                           "teemo", "udyr", "yasuo"
                                                                       };

        /// <summary>
        ///     A list containing the names of the champs whose ultimate is useless to track.
        /// </summary>
        public static readonly List<string> RealSpells = new List<string>
                                                             {
                                                                 "AhriTumble", "InfernalGuardian",
                                                                 "IreliaTranscendentBladesSpell", "JhinR",
                                                                 "RivenIzunaBlade", "HallucinateFull", "ZedR"
                                                             };

        #endregion

        #region Public Properties

        /// <summary>
        ///     The Hero Menu.
        /// </summary>
        public static Menu HeroMenu { internal get; set; }

        /// <summary>
        ///     The Menu.
        /// </summary>
        public static Menu Menu { internal get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the enemy names.
        /// </summary>
        public static string GetHumanName(string str)
        {
            switch (str.ToLower())
            {
                case "drmundo":
                    str = "mundo";
                    break;
                case "masteryi":
                    str = "yi";
                    break;
                case "khazix":
                    str = "kha";
                    break;
                case "kogmaw":
                    str = "kog";
                    break;
                case "chogath":
                    str = "cho";
                    break;
                case "aurelionsol":
                    str = "aurelion";
                    break;
                case "cassiopeia":
                    str = "cass";
                    break;
                case "caitlyn":
                    str = "cait";
                    break;
                case "blitzcrank":
                    str = "blitz";
                    break;
                case "evelynn":
                    str = "eve";
                    break;
                case "alistar":
                    str = "ali";
                    break;
                case "ezreal":
                    str = "ez";
                    break;
                case "fiddlesticks":
                    str = "fid";
                    break;
                case "hecarim":
                    str = "heca";
                    break;
                case "gangplank":
                    str = "gp";
                    break;
                case "heimerdinger":
                    str = "heimer";
                    break;
                case "jarvaniv":
                    str = "j4";
                    break;
                case "kalista":
                    str = "kal";
                    break;
                case "katarina":
                    str = "kat";
                    break;
                case "kassadin":
                    str = "kass";
                    break;
                case "leblanc":
                    str = "lb";
                    break;
                case "leesin":
                    str = "lee";
                    break;
                case "lissandra":
                    str = "liss";
                    break;
                case "lucian":
                    str = "luc";
                    break;
                case "malphite":
                    str = "malph";
                    break;
                case "missfortune":
                    str = "mf";
                    break;
                case "mordekaiser":
                    str = "morde";
                    break;
                case "morgana":
                    str = "morg";
                    break;
                case "nautilus":
                    str = "nauti";
                    break;
                case "nidalee":
                case "nidaleecougar":
                    str = "nida";
                    break;
                case "nocturne":
                    str = "noc";
                    break;
                case "orianna":
                    str = "ori";
                    break;
                case "pantheon":
                    str = "panth";
                    break;
                case "reksai":
                    str = "rek";
                    break;
                case "renekton":
                    str = "renek";
                    break;
                case "sejuani":
                    str = "sej";
                    break;
                case "shyvana":
                case "shyvanadragon":
                    str = "shyv";
                    break;
                case "tahmkench":
                    str = "tahm";
                    break;
                case "tryndamere":
                    str = "trynd";
                    break;
                case "twistedfate":
                    str = "tf";
                    break;
                case "viktor":
                    str = "vik";
                    break;
                case "volibear":
                    str = "voli";
                    break;
                case "warwick":
                    str = "ww";
                    break;
                case "monkeyking":
                    str = "wuk";
                    break;
                case "yasuo":
                    str = "yas";
                    break;
                case "xinzhao":
                    str = "xin";
                    break;
                case "zilean":
                    str = "zil";
                    break;
                default:
                    str = str.ToLower();
                    break;
            }

            return str;
        }

        /// <summary>
        ///     Initializes the spell names.
        /// </summary>
        public static string GetHumanSpellName(string str)
        {
            switch (str.ToLower())
            {
                case "summonerflash":
                    str = "f";
                    break;
                case "summonerdot":
                    str = "ignite";
                    break;
                case "summonerheal":
                    str = "heal";
                    break;
                case "summonerteleport":
                    str = "tp";
                    break;
                case "summonerexhaust":
                    str = "exh";
                    break;
                case "summonerhaste":
                    str = "ghost";
                    break;
                case "summonerbarrier":
                    str = "barrier";
                    break;
                case "summonerboost":
                    str = "cleanse";
                    break;
                default:
                    str = null;
                    break;
            }

            return str;
        }

        #endregion
    }
}