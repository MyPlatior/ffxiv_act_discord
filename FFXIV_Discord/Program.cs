using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sharlayan;
using Sharlayan.Core;
using Sharlayan.Models;
using Sharlayan.Models.ReadResults;
using Sharlayan.Utilities;
using DiscordRPC;
using DiscordRPC.Logging;
using Advanced_Combat_Tracker;
using System.Windows.Forms;
using System.Timers;

namespace FFXIV_Discord
{
    class Program : IActPluginV1
    {

        private static System.Timers.Timer timer;
        private static DiscordRpcClient discord;
        private static bool gameRunning;
        private static Timestamps discordStartTime;

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {

            gameRunning = Attach();

            //CONNECT TO CANARY INSTEAD OF NORMAL DISC
            //System.Environment.SetEnvironmentVariable("DISCORD_INSTANCE_ID", "0");

            //discord.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            ////Subscribe to events
            //discord.OnReady += (sender, e) =>
            //{
            //    Console.WriteLine("Received Ready from user {0}", e.User.Username);
            //};

            //discord.OnPresenceUpdate += (sender, e) =>
            //{
            //    Console.WriteLine("Received Update! {0}", e.Presence);
            //};

            discord = new DiscordRpcClient("590267016842051654");

            //Connect to the RPC
            if (gameRunning)
            {
                //start discord
                discordStartTime = Timestamps.Now;
                discord.Initialize();
            }

            SetTimer();

            pluginStatusText.Text = "Loaded";

        }

        public void DeInitPlugin()
        {
            discord.Dispose();
            timer.Dispose();
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += UpdateRPC;
            timer.AutoReset = true;
            timer.Start();
        }

        private static bool Attach()
        {
            if (MemoryHandler.Instance.IsAttached)
            {
                return true;
            }

            //try reattach
            Process[] processes = Process.GetProcessesByName("ffxiv_dx11");
            if (processes.Length != 0)
            {
                Process process = processes[0];
                ProcessModel processModel = new ProcessModel
                {
                    Process = process,
                    IsWin64 = true
                };
                MemoryHandler.Instance.SetProcess(processModel, "English");
                return true;
            }
            return false;
        }

        private static void UpdateRPC(Object source, ElapsedEventArgs e)
        {
            gameRunning = Attach();

            if (gameRunning)
            {
                string smallImageText = null;
                string smallImageKey = null;
                string details;
                string status;

                Reader.GetActors();
                CurrentPlayerResult cpr = Reader.GetCurrentPlayer();
                var player = ActorItem.CurrentUser;

                
                if (cpr.CurrentPlayer.Name != null && cpr.CurrentPlayer.Name != "" && player != null)
                {
                    details = String.Format("{0} ({1} Lv{2})", cpr.CurrentPlayer.Name, UIStrings.JobAbbreviations[player.Job], player.Level);
                    smallImageKey = UIStrings.JobAbbreviations[player.Job].ToLower();
                    smallImageText = String.Format("Level {0} {1}", player.Level, UIStrings.JobNames[player.Job]);
                    string zone = ActGlobals.oFormActMain.CurrentZone;

                    switch (player.IconID)
                    {
                        case 15:
                            status = String.Format("Watching a cutscene");
                            break;
                        case 17:
                            status = String.Format("AFK in {0}", zone);
                            break;
                        case 18:
                            status = String.Format("Taking screenshots");
                            break;
                        case 22:
                            status = String.Format("RP'ing in {0}", zone);
                            break;
                        case 25:
                            status = String.Format("Waiting for Duty Finder");
                            break;
                        case 26:
                            status = String.Format("Recruiting Party Members");
                            break;
                        default:
                            status = zone;
                            break;
                    }

                }
                else
                {
                    details = "In Menus";
                    status = null;
                }

                if (discord.IsInitialized)
                {
                    discord.SetPresence(new RichPresence()
                    {
                        Details = details,
                        State = status,
                        Assets = new Assets()
                        {
                            LargeImageKey = "logo",
                            SmallImageKey = smallImageKey,
                            SmallImageText = smallImageText
                        },
                        Timestamps = discordStartTime
                    });
                }
            }

            if (!gameRunning && !discord.IsDisposed)
            {
                //dispose discord if game is not running
                discord.Dispose();
            }
            if (gameRunning && !discord.IsInitialized)
            {
                //start discord if not running and game is running
                discord = new DiscordRpcClient("590267016842051654");
                discordStartTime = Timestamps.Now;
                discord.Initialize();
            }

        }


        internal static class UIStrings
        {
            public readonly static Dictionary<Sharlayan.Core.Enums.Actor.Job, string> JobNames = new Dictionary<Sharlayan.Core.Enums.Actor.Job, string>
            {
                { Sharlayan.Core.Enums.Actor.Job.Unknown, "Unknown"},
                //classes
                { Sharlayan.Core.Enums.Actor.Job.GLD, "Gladiator" },
                { Sharlayan.Core.Enums.Actor.Job.PGL, "Pugilist" },
                { Sharlayan.Core.Enums.Actor.Job.MRD, "Marauder" },
                { Sharlayan.Core.Enums.Actor.Job.LNC, "Lancer" },
                { Sharlayan.Core.Enums.Actor.Job.ARC, "Archer" },
                { Sharlayan.Core.Enums.Actor.Job.CNJ, "Conjurer" },
                { Sharlayan.Core.Enums.Actor.Job.THM, "Thaumaturge" },
                { Sharlayan.Core.Enums.Actor.Job.ACN, "Arcanist" },
                { Sharlayan.Core.Enums.Actor.Job.ROG, "Rogue" },
                //crafters
                { Sharlayan.Core.Enums.Actor.Job.CPT, "Carpenter" },
                { Sharlayan.Core.Enums.Actor.Job.BSM, "Blacksmith" },
                { Sharlayan.Core.Enums.Actor.Job.ARM, "Armorer" },
                { Sharlayan.Core.Enums.Actor.Job.GSM, "Goldsmith" },
                { Sharlayan.Core.Enums.Actor.Job.LTW, "Leatherworker" },
                { Sharlayan.Core.Enums.Actor.Job.WVR, "Weaver" },
                { Sharlayan.Core.Enums.Actor.Job.ALC, "Alchemist" },
                { Sharlayan.Core.Enums.Actor.Job.CUL, "Culinarian" },
                //gatherers
                { Sharlayan.Core.Enums.Actor.Job.MIN, "Miner" },
                { Sharlayan.Core.Enums.Actor.Job.FSH, "Fisher" },
                { Sharlayan.Core.Enums.Actor.Job.BTN, "Botanist" },
                //jobs
                { Sharlayan.Core.Enums.Actor.Job.PLD, "Paladin" },
                { Sharlayan.Core.Enums.Actor.Job.WAR, "Warrior" },
                { Sharlayan.Core.Enums.Actor.Job.DRK, "Dark Knight" },
                { Sharlayan.Core.Enums.Actor.Job.WHM, "White Mage" },
                { Sharlayan.Core.Enums.Actor.Job.SCH, "Scholar" },
                { Sharlayan.Core.Enums.Actor.Job.AST, "Astrologian" },
                { Sharlayan.Core.Enums.Actor.Job.MNK, "Monk" },
                { Sharlayan.Core.Enums.Actor.Job.DRG, "Dragoon" },
                { Sharlayan.Core.Enums.Actor.Job.NIN, "Ninja" },
                { Sharlayan.Core.Enums.Actor.Job.SAM, "Samurai" },
                { Sharlayan.Core.Enums.Actor.Job.BRD, "Bard" },
                { Sharlayan.Core.Enums.Actor.Job.MCH, "Machinist" },
                { Sharlayan.Core.Enums.Actor.Job.BLM, "Black Mage" },
                { Sharlayan.Core.Enums.Actor.Job.SMN, "Summoner" },
                { Sharlayan.Core.Enums.Actor.Job.RDM, "Red Mage" },
                { Sharlayan.Core.Enums.Actor.Job.BLU, "Blue Mage" }
                //{ Sharlayan.Core.Enums.Actor.Job.GBR, "Gunbreaker" }
                //{ Sharlayan.Core.Enums.Actor.Job.DNC, "Dancer" }
            };
            public readonly static Dictionary<Sharlayan.Core.Enums.Actor.Job, string> JobAbbreviations = new Dictionary<Sharlayan.Core.Enums.Actor.Job, string>
            {
                { Sharlayan.Core.Enums.Actor.Job.Unknown, "???"},
                //classes
                { Sharlayan.Core.Enums.Actor.Job.GLD, "GLA" },
                { Sharlayan.Core.Enums.Actor.Job.PGL, "PGL" },
                { Sharlayan.Core.Enums.Actor.Job.MRD, "MRD" },
                { Sharlayan.Core.Enums.Actor.Job.LNC, "LNC" },
                { Sharlayan.Core.Enums.Actor.Job.ARC, "ARC" },
                { Sharlayan.Core.Enums.Actor.Job.ACN, "ACN" },
                { Sharlayan.Core.Enums.Actor.Job.CNJ, "CNJ" },
                { Sharlayan.Core.Enums.Actor.Job.THM, "THM" },
                { Sharlayan.Core.Enums.Actor.Job.ROG, "ROG" },
                //crafters
                { Sharlayan.Core.Enums.Actor.Job.CPT, "CRP" },
                { Sharlayan.Core.Enums.Actor.Job.BSM, "BSM" },
                { Sharlayan.Core.Enums.Actor.Job.ARM, "ARM" },
                { Sharlayan.Core.Enums.Actor.Job.GSM, "GSM" },
                { Sharlayan.Core.Enums.Actor.Job.LTW, "LTW" },
                { Sharlayan.Core.Enums.Actor.Job.WVR, "WVR" },
                { Sharlayan.Core.Enums.Actor.Job.ALC, "ALC" },
                { Sharlayan.Core.Enums.Actor.Job.CUL, "CUL" },
                //gatherers
                { Sharlayan.Core.Enums.Actor.Job.MIN, "MIN" },
                { Sharlayan.Core.Enums.Actor.Job.FSH, "FSH" },
                { Sharlayan.Core.Enums.Actor.Job.BTN, "BTN" },
                //jobs
                { Sharlayan.Core.Enums.Actor.Job.PLD, "PLD" },
                { Sharlayan.Core.Enums.Actor.Job.WAR, "WAR" },
                { Sharlayan.Core.Enums.Actor.Job.DRK, "DRK" },
                { Sharlayan.Core.Enums.Actor.Job.WHM, "WHM" },
                { Sharlayan.Core.Enums.Actor.Job.SCH, "SCH" },
                { Sharlayan.Core.Enums.Actor.Job.AST, "AST" },
                { Sharlayan.Core.Enums.Actor.Job.MNK, "MNK" },
                { Sharlayan.Core.Enums.Actor.Job.DRG, "DRG" },
                { Sharlayan.Core.Enums.Actor.Job.NIN, "NIN" },
                { Sharlayan.Core.Enums.Actor.Job.SAM, "SAM" },
                { Sharlayan.Core.Enums.Actor.Job.BRD, "BRD" },
                { Sharlayan.Core.Enums.Actor.Job.MCH, "MCH" },
                { Sharlayan.Core.Enums.Actor.Job.BLM, "BLM" },
                { Sharlayan.Core.Enums.Actor.Job.SMN, "SMN" },
                { Sharlayan.Core.Enums.Actor.Job.RDM, "RDM" },
                { Sharlayan.Core.Enums.Actor.Job.BLU, "BLU" }
                //{ Sharlayan.Core.Enums.Actor.Job.GBR, "GBR" }
                //{ Sharlayan.Core.Enums.Actor.Job.DNC, "DNC" }
            };

        }
    }
}
