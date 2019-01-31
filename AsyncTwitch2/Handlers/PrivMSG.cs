using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTwitch;
using AsyncTwitch.Models;

namespace AsyncTwitch.Handlers
{
    internal static class PrivMsg
    {
        public static void Handle(RawMessage rawMessage)
        {
            TwitchMessage message = Parsers.ParseTwitchMessage(rawMessage.Message);
            TwitchConnection.OnMessage?.Invoke(TwitchConnection.Instance, message);
        }
    }
}
