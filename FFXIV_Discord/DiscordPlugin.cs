using System;
using Sharlayan;
using Sharlayan.Core;
using Sharlayan.Models;
using Sharlayan.Models.ReadResults;
using DiscordRPC;
using Advanced_Combat_Tracker;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;

namespace FFXIV_Discord
{
    class DiscordPlugin : IDisposable
    {
        string pluginDirectory;

        TabPage pluginScreenSpace;
        Label pluginStatusText;

        private System.Timers.Timer timer;
        private DiscordRpcClient discord;
        private Timestamps discordStartTime;

        private const string DISCORD_CLIENT_ID = "590267016842051654";

        public DiscordPlugin(string pluginDirectory)
        {
            this.pluginDirectory = pluginDirectory;
        }

        public void Init(TabPage pluginScreenSpace, Label pluginStatusText)
        {

            this.pluginScreenSpace = pluginScreenSpace;
            this.pluginStatusText = pluginStatusText;

            discord = new DiscordRpcClient(DISCORD_CLIENT_ID);

            ////Connect to the RPC
            //if (Attach())
            //{
            //    //start discord
            //    discordStartTime = Timestamps.Now;
            //    discord.Initialize();
            //}

            SetTimer();

            pluginStatusText.Text = "Plugin Loaded";

        }

        public void Dispose()
        {
            discord.Dispose();
            timer.Dispose();

            pluginStatusText.Text = "Plugin Unloaded";
        }

        private void SetTimer()
        {
            // timer with a 5 second interval.
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += UpdateRPC;
            timer.AutoReset = true;
            timer.Start();
        }

        private bool Attach()
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

        private void UpdateRPC(Object source, ElapsedEventArgs e)
        {

            if (Attach())
            {

                if (!discord.IsInitialized)
                {
                    //start discord if not running and game is running
                    discord = new DiscordRpcClient(DISCORD_CLIENT_ID);
                    discordStartTime = Timestamps.Now;
                    discord.Initialize();
                }

                string smallImageText = null;
                string smallImageKey = null;
                string details;
                string status;

                Reader.GetActors();
                CurrentPlayerResult cpr = Reader.GetCurrentPlayer();
                var player = ActorItem.CurrentUser;


                if (cpr.CurrentPlayer.Name != null && cpr.CurrentPlayer.Name != "" && player != null)
                {
                    details = String.Format("{0} ({1} Lv{2})", cpr.CurrentPlayer.Name, UIStrings.JobAbbreviations[cpr.CurrentPlayer.Job], player.Level);
                    smallImageKey = UIStrings.JobAbbreviations[cpr.CurrentPlayer.Job].ToLower();
                    smallImageText = String.Format("Level {0} {1}", player.Level, UIStrings.JobNames[cpr.CurrentPlayer.Job]);

                    string zone = ActGlobals.oFormActMain.CurrentZone;
                    pluginStatusText.Text = String.Format("Character Found: {0}", details);

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
                    details = "Main Menu";
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
            else
            {
                if (!discord.IsDisposed)
                {
                    //dispose discord if game is not running
                    discord.Dispose();
                }
            }
        }
    }
}
