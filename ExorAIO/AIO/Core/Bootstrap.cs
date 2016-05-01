using ExorAIO.Champions.Akali;
using ExorAIO.Champions.Amumu;
using ExorAIO.Champions.Anivia;
using ExorAIO.Champions.Ashe;
using ExorAIO.Champions.Caitlyn;
using ExorAIO.Champions.Cassiopeia;
using ExorAIO.Champions.Corki;
using ExorAIO.Champions.Darius;
using ExorAIO.Champions.Draven;
using ExorAIO.Champions.DrMundo;
using ExorAIO.Champions.Evelynn;
using ExorAIO.Champions.Ezreal;
using ExorAIO.Champions.Graves;
using ExorAIO.Champions.Jax;
using ExorAIO.Champions.Jhin;
using ExorAIO.Champions.Jinx;
using ExorAIO.Champions.Kalista;
using ExorAIO.Champions.KogMaw;
using ExorAIO.Champions.Lucian;
using ExorAIO.Champions.Lux;
using ExorAIO.Champions.Nautilus;
using ExorAIO.Champions.Nunu;
using ExorAIO.Champions.Olaf;
using ExorAIO.Champions.Pantheon;
using ExorAIO.Champions.Quinn;
using ExorAIO.Champions.Renekton;
using ExorAIO.Champions.Ryze;
using ExorAIO.Champions.Sivir;
/*
using ExorAIO.Champions.Tristana;
using ExorAIO.Champions.Tryndamere;
using ExorAIO.Champions.Twitch;
using ExorAIO.Champions.Udyr;
using ExorAIO.Champions.Warwick;
*/
using ExorAIO.Champions.Vayne;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Core
{
    /// <summary>
    ///     The bootstrap class.
    /// </summary>
    internal class Bootstrap
    {
        /// <summary>
        ///     Tries to load the champion which is being currently played.
        /// </summary>
        public static void LoadChampion()
        {
            switch (GameObjects.Player.ChampionName)
            {
                case "Akali":
                    new Akali().OnLoad();
                    break;
                case "Amumu":
                    new Amumu().OnLoad();
                    break;
                case "Anivia":
                    new Anivia().OnLoad();
                    break;
                case "Ashe":
                    new Ashe().OnLoad();
                    break;
                case "Caitlyn":
                    new Caitlyn().OnLoad();
                    break;
                case "Cassiopeia":
                    new Cassiopeia().OnLoad();
                    break;
                case "Corki":
                    new Corki().OnLoad();
                    break;
                case "Darius":
                    new Darius().OnLoad();
                    break;
                case "Draven":
                    new Draven().OnLoad();
                    break;
                case "DrMundo":
                    new DrMundo().OnLoad();
                    break;
                case "Evelynn":
                    new Evelynn().OnLoad();
                    break;
                case "Ezreal":
                    new Ezreal().OnLoad();
                    break;
                case "Graves":
                    new Graves().OnLoad();
                    break;
                case "Jax":
                    new Jax().OnLoad();
                    break;
                case "Jhin":
                    new Jhin().OnLoad();
                    break;
                case "Jinx":
                    new Jinx().OnLoad();
                    break;
                case "Kalista":
                    new Kalista().OnLoad();
                    break;
                case "KogMaw":
                    new KogMaw().OnLoad();
                    break;
                case "Lucian":
                    new Lucian().OnLoad();
                    break;
                case "Lux":
                    new Lux().OnLoad();
                    break;
                case "Nautilus":
                    new Nautilus().OnLoad();
                    break;
                case "Nunu":
                    new Nunu().OnLoad();
                    break;
                case "Olaf":
                    new Olaf().OnLoad();
                    break;
                case "Pantheon":
                    new Pantheon().OnLoad();
                    break;
                case "Quinn":
                    new Quinn().OnLoad();
                    break;
                case "Renekton":
                    new Renekton().OnLoad();
                    break;
                case "Ryze":
                    new Ryze().OnLoad();
                    break;
                case "Sivir":
                    new Sivir().OnLoad();
                    break;
                /*
                case "Tristana":
                    new Tristana().OnLoad();
                    break;
                case "Tryndamere":
                    new Tryndamere().OnLoad();
                    break;
                case "Twitch":
                    new Twitch().OnLoad();
                    break;
                case "Udyr":
                    new Udyr().OnLoad();
                    break;
                case "Warwick":
                    new Warwick().OnLoad();
                    break;
                */
                case "Vayne":
                    new Vayne().OnLoad();
                    break;
                default:
                    Vars.IsLoaded = false;
                    break;
            }

            Game.PrintChat(
                $"[SDK]<b><font color='#009aff'>Exor</font></b>AIO: <font color='#009aff'>Ultima</font> - {GameObjects.Player.ChampionName} " + (Vars.IsLoaded 
                    ? "Loaded." 
                    : "is not supported."));
        }
    }
}