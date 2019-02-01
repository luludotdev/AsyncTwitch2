using System;
using IllusionPlugin;

namespace AsyncTwitch
{
    class Plugin : IPlugin
    {
        #region Metadata
        public static string PluginName = "AsyncTwitch";
        public static string PluginVersion = "2.0.0";

        public string Name => PluginName;
        public string Version => PluginVersion;
        #endregion

        #region Logging
        public static void Log(object data)
        {
            Console.WriteLine($"[{PluginName}] {data}");
        }

        public static void Debug(object data)
        {
#if DEBUG
            Console.WriteLine($"[{PluginName} DEBUG] {data}");
#endif
        }
        #endregion

        public void OnApplicationStart()
        {
            Config.Generate();
            TwitchConnection.OnLoad();
        }

        #region Unused
        public void OnApplicationQuit() { }

        public void OnLevelWasInitialized(int level) { }

        public void OnLevelWasLoaded(int level) { }

        public void OnUpdate() { }

        public void OnFixedUpdate() { }
        #endregion
    }
}
