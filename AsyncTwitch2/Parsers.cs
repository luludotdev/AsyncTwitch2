using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AsyncTwitch.Models;

namespace AsyncTwitch
{
    internal static class Parsers
    {
        private static readonly Regex _twitchMessageRegex = new Regex(@":(?<HostName>[\S]+) (?<MessageType>[\S]+) #(?<ChannelName>[\S]+)");
        private static readonly Regex _messageRegex = new Regex(@" #[\S]+ :(?<Message>.*)");
        private static readonly Regex _tagRegex = new Regex(@"(?<Tag>[a-z,0-9,-]+)=(?<Value>[^;\s]+)");

        public static bool ValidRawMessage(string raw)
        {
            Match parsed = _twitchMessageRegex.Match(raw);
            return parsed.Length > 0;
        }

        public static RawMessage ParseRawMessage(string raw)
        {
            Match parsed = _twitchMessageRegex.Match(raw);

            string Hostname = parsed.Groups["HostName"].Value;
            string Type = parsed.Groups["MessageType"].Value;
            string ChannelName = parsed.Groups["ChannelName"].Value;

            return new RawMessage(Hostname, Type, ChannelName, raw);
        }
    }
}
