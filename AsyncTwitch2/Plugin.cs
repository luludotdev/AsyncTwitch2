using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IllusionPlugin;

namespace AsyncTwitch
{
    class Plugin : IPlugin
    {
        public static string PluginName = "AsyncTwitch";
        public static string PluginVersion = "2.0.0";

        public string Name => PluginName;
        public string Version => PluginVersion;

        public static void Log(object data)
        {
            Console.WriteLine($"[{PluginName}] {data}");
        }

        public static void Debug(object data)
        {
#if DEBUG
            Console.WriteLine($"[{PluginName}] {data}");
#endif
        }

        public void OnApplicationStart()
        {
            Config.Generate();
            TwitchConnection.OnLoad();
        }

        public void OnApplicationQuit() { }

        public void OnLevelWasInitialized(int level) { }

        public void OnLevelWasLoaded(int level) { }

        public void OnUpdate() { }

        public void OnFixedUpdate() { }
    }
}
