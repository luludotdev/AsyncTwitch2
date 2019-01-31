using System;
using System.Linq;
using UnityEngine;
using WebSocketSharp;
using AsyncTwitch.Models;

namespace AsyncTwitch
{
    public class TwitchConnection : MonoBehaviour
    {
        public static TwitchConnection Instance;

        private static WebSocket _ws;
        private static System.Random _random = new System.Random();

        public static void OnLoad()
        {
            if (Instance != null)
                return;

            new GameObject("AsyncTwitch").AddComponent<TwitchConnection>();
        }

        public void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Plugin.Debug("Created AsyncTwitch Gameobject");

            _ws = new WebSocket("wss://irc-ws.chat.twitch.tv");

            _ws.OnOpen += (sender, ev) =>
            {
                Plugin.Debug("WebSocket Connection Opened");

                _ws.Send("CAP REQ :twitch.tv/tags twitch.tv/commands twitch.tv/membership");
                
                if (Config.Username == "" || Config.OAuthKey == "")
                {
                    int id = _random.Next(10000, 1000000);

                    _ws.Send($"NICK justinfan{id}");
                    _ws.Send($"PASS {id}");
                }
                else
                {
                    _ws.Send($"NICK {Config.Username}");
                    _ws.Send($"PASS {Config.OAuthKey}");
                }

                string channel = Config.ChannelName == "" ? Config.Username : Config.ChannelName;
                if (channel != "")
                    _ws.Send($"JOIN #{channel}");
            };

            _ws.OnClose += (sender, ev) =>
            {
                Plugin.Debug($"Socket Closed with reason {ev.Reason}");
            };

            _ws.OnMessage += _ws_OnMessage;
            _ws.ConnectAsync();
        }

        private void _ws_OnMessage(object sender, MessageEventArgs ev)
        {
            if (!ev.IsText)
                return;

            string message = ev.Data.TrimEnd();

#if DEBUG
            string[] lines = message
                .Split('\n')
                .Select((line, i) =>
                {
                    string prefix = "Twitch";
                    string pre = i == 0 ? "Twitch" : new string(' ', prefix.Length);

                    return $"{pre} | {line}";
                })
                .ToArray();

            Console.WriteLine(string.Join("\n", lines));
#endif

            if (message.StartsWith("PING"))
            {
                Plugin.Debug("Recieved PING. Sending PONG...");
                _ws.Send("PONG :tmi.twitch.tv");

                return;
            }

            bool valid = Parsers.ValidRawMessage(message);
            if (!valid)
            {
                Plugin.Debug($"Unhandled message: {message}");
                return;
            }

            RawMessage rawMessage = Parsers.ParseRawMessage(message);
            Plugin.Debug($"Hostname: {rawMessage.Hostname}");
            Plugin.Debug($"Channel Name: {rawMessage.ChannelName}");
            Plugin.Debug($"Type: {rawMessage.Type}");
        }
    }
}
