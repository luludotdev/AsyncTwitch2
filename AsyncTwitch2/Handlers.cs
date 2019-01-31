using System;
using AsyncTwitch.Models;

namespace AsyncTwitch
{
    internal static class Handlers
    {
        public static void PRIVMSG(RawMessage rawMessage)
        {
            TwitchMessage message = Parsers.ParseTwitchMessage(rawMessage.Message);
            TwitchConnection.OnMessage?.Invoke(TwitchConnection.Instance, message);
        }

        public static void ROOMSTATE(RawMessage rawMessage)
        {

        }
    }
}
