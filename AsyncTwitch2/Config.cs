using System;

namespace AsyncTwitch
{
    internal static class Config
    {
        private static BS_Utils.Utilities.Config TwitchConfig = new BS_Utils.Utilities.Config("AsyncTwitch");
        private static readonly string TwitchSection = "Twitch";

        public static string Username
        {
            get => TwitchConfig.GetString(TwitchSection, "Username", "", true);
            set => TwitchConfig.SetString(TwitchSection, "Username", value);
        }

        public static string ChannelName
        {
            get => TwitchConfig.GetString(TwitchSection, "ChannelName", "", true);
            set => TwitchConfig.SetString(TwitchSection, "ChannelName", value);
        }

        public static string OAuthKey
        {
            get
            {
                string key = TwitchConfig.GetString(TwitchSection, "OAuthKey", "", true);
                return key.StartsWith("oauth:") ? key : $"oauth:{key}";
            }
            set => TwitchConfig.SetString(TwitchSection, "OAuthKey", value);
        }

        public static void Generate()
        {
            TwitchConfig.GetString(TwitchSection, "Username", "", true);
            TwitchConfig.GetString(TwitchSection, "ChannelName", "", true);
            TwitchConfig.GetString(TwitchSection, "OAuthKey", "", true);
        }
    }
}
