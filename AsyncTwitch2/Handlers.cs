using System;
using AsyncTwitch.Misc;
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
            RoomState roomState = TwitchConnection.RoomStates.ContainsKey(rawMessage.ChannelName) ?
                TwitchConnection.RoomStates[rawMessage.ChannelName] :
                new RoomState(rawMessage.ChannelName);

            var tags = Parsers.ParseTags(rawMessage.Message);
            roomState.ID = tags["room-id"];

            string lang = tags.GetValue("broadcaster-lang");
            if (lang != null) roomState.Language = lang;

            string emoteOnly = tags.GetValue("emote-only");
            if (emoteOnly != null) roomState.EmoteOnly = emoteOnly == "1";

            string subsOnly = tags.GetValue("subs-only");
            if (subsOnly != null) roomState.SubsOnly = subsOnly == "1";

            string followersOnly = tags.GetValue("followers-only");
            if (followersOnly != null) roomState.FollowersOnly = Convert.ToInt32(followersOnly);

            string slowMode = tags.GetValue("slow");
            if (slowMode != null) roomState.SlowMode = Convert.ToUInt16(slowMode);

            string r9k = tags.GetValue("r9k");
            if (r9k != null) roomState.R9K = r9k == "1";

            TwitchConnection.RoomStates[rawMessage.ChannelName] = roomState;
            TwitchConnection.OnRoomStateChange?.Invoke(TwitchConnection.Instance, roomState);
        }
    }
}
