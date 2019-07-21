using System;
using Advanced_Combat_Tracker;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using System.IO;

namespace FFXIV_Discord
{
    public class PluginLoader : IActPluginV1
    {

        DiscordPlugin discordPlugin;
        string pluginDirectory;

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            //get the working directory of the plugin
            var plugin = ActGlobals.oFormActMain.ActPlugins.Where(x => x.pluginObj == this).FirstOrDefault();
            pluginDirectory = (plugin != null) ? Path.GetDirectoryName(plugin.pluginFile.FullName) : throw new Exception();

            AppDomain.CurrentDomain.AssemblyResolve += Resolver;

            discordPlugin = new DiscordPlugin(pluginDirectory);
            discordPlugin.Init(pluginScreenSpace, pluginStatusText);


        }

        public Assembly Resolver(object sender, ResolveEventArgs args)
        {
            var asmName = new AssemblyName(args.Name).Name;

            if (!asmName.EndsWith(".dll")) {
                asmName += ".dll";
            }

            var asmPath = Path.Combine(pluginDirectory, asmName);
            if (File.Exists(asmPath)) {
                return Assembly.LoadFile(asmPath);
            }

            return null;
        }


        public void DeInitPlugin()
        {
            discordPlugin.Dispose();
        }

    }
}
