using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AsyncTwitch.Misc;
using AsyncTwitch.Models;

namespace AsyncTwitch
{
    internal static class Parsers
    {
        private static readonly Regex _twitchMessageRegex = new Regex(@":(?<HostName>[\S]+) (?<MessageType>[\S]+) #(?<ChannelName>[\S]+)");
        private static readonly Regex _usernameRegex = new Regex(@":(?<Username>[\S]+)!");
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

        public static TwitchMessage ParseTwitchMessage(string raw)
        {
            Match message = _messageRegex.Match(raw);
            Dictionary<string, string> tags = ParseTags(raw);

            string content = message.Groups["Message"].Value;
            int cheerAmount = Convert.ToInt32(tags.GetValue("bits", "-1"));
            DateTime timestamp = Convert.ToInt64(tags["tmi-sent-ts"]).FromUnixTime();
            ChatUser author = ParseChatUser(raw, tags);

            return new TwitchMessage(content, raw)
            {
                Author = author,
                ID = tags["id"],
                Cheer = cheerAmount > 0,
                CheerAmount = cheerAmount,
                Emotes = new Emote[0],
                Timestamp = timestamp,
            };
        }

        public static ChatUser ParseChatUser(string raw)
        {
            Dictionary<string, string> tags = ParseTags(raw);
            return ParseChatUser(raw, tags);
        }

        public static ChatUser ParseChatUser(string raw, Dictionary<string, string> tags)
        {
            string username = _usernameRegex.Match(raw).Groups["Username"].Value;
            string displayName = tags["display-name"];
            string id = tags["id"];
            string color = tags["color"];

            ChatBadge[] badges = tags["badges"]
                .Split(',')
                .Select(x =>
                {
                    string[] badge = x.Split('/');

                    string name = badge[0];
                    short version = Convert.ToInt16(badge[1]);

                    return new ChatBadge(name, version);
                })
                .ToArray();

            Dictionary<string, ChatBadge> badgesDict = badges.ToDictionary(x => x.Name);

            bool broadcaster = badgesDict.ContainsKey("broadcaster");
            bool globalMod = badgesDict.ContainsKey("global_mod");
            bool moderator = badgesDict.ContainsKey("moderator");
            bool subscriber = badgesDict.ContainsKey("subscriber");
            bool vip = badgesDict.ContainsKey("vip");

            bool isMod = globalMod || broadcaster || moderator;

            return new ChatUser(username, displayName, id, color, isMod, broadcaster, subscriber, vip, badges);
        }

        public static Dictionary<string, string> ParseTags(string raw)
        {
            Dictionary<string, string> parsed = new Dictionary<string, string>();
            MatchCollection tags = _tagRegex.Matches(raw);

            foreach (Match t in tags)
            {
                string tag = t.Groups["Tag"].Value;
                string value = t.Groups["Value"].Value;

                parsed.Add(tag, value);
            }

            return parsed;
        }
    }
}
