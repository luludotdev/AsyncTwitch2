using System;
using System.Linq;
using UnityEngine;
using WebSocketSharp;

namespace AsyncTwitch
{
    public class TwitchConnection : MonoBehaviour
    {
        public static TwitchConnection Instance;

        public static WebSocket _ws;

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
                _ws.Send("NICK justinfan123456");
                _ws.Send("PASS bfuyesfhgkesl");
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
        }
    }
}
