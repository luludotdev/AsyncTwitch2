using System.Collections.Generic;
using System.Text;

namespace AsyncTwitch.Models
{
    public struct RoomState
    {
        /// <summary>
        /// Name of the channel. Omits the leading # character.
        /// </summary>
        public string Name;

        /// <summary>
        /// Unique identifier for this room.
        /// </summary>
        public string ID;

        /// <summary>
        /// Language set by the broacaster. EG: <code>en</code> or <code>fi</code>
        /// </summary>
        public string Language;

        /// <summary>
        /// Whether this room is in Emotes Only mode.
        /// </summary>
        public bool EmoteOnly;

        /// <summary>
        /// Whether this room is in Subscriber Only mode.
        /// </summary>
        public bool SubsOnly;

        /// <summary>
        /// Whether this room is in Followers Only mode.
        /// 
        /// Valid values are:
        /// -1 - Disabled.
        /// 0 - All followers can chat.
        /// >0 - number of minutes until a new follower can chat.
        /// </summary>
        public int FollowersOnly;

        /// <summary>
        /// Number of seconds a user must wait between sending messages.
        /// </summary>
        public int SlowMode;

        /// <summary>
        /// Whether this room has Robot9000 Mode enabled.
        /// </summary>
        public bool R9K;

        /// <summary>
        /// List of all users seen in this room.
        /// </summary>
        public List<ChatUser> Users;

        public RoomState(string _name, string _roomID, string _language, bool _emoteOnly, bool _subsOnly, int _followersOnly, int _slowMode, bool _r9k)
        {
            Name = _name;
            ID = _roomID;
            Language = _language;
            EmoteOnly = _emoteOnly;
            SubsOnly = _subsOnly;
            FollowersOnly = _followersOnly;
            SlowMode = _slowMode;
            R9K = _r9k;
            Users = new List<ChatUser>();
        }

        public RoomState(string _name, string _roomID, string _language, bool _emoteOnly, bool _subsOnly, int _followersOnly, int _slowMode, bool _r9k, List<ChatUser> _users)
        {
            Name = _name;
            ID = _roomID;
            Language = _language;
            EmoteOnly = _emoteOnly;
            SubsOnly = _subsOnly;
            FollowersOnly = _followersOnly;
            SlowMode = _slowMode;
            R9K = _r9k;
            Users = _users;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-- Room State --");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine($"Language: {Language}");
            sb.AppendLine($"Emote Only Mode: {EmoteOnly}");
            sb.AppendLine($"Subscriber Only Mode: {SubsOnly}");
            sb.AppendLine($"Follower Only Mode: {FollowersOnly}");
            sb.AppendLine($"Slow Mode Timeout: {SlowMode}");
            sb.AppendLine($"Robot9000 Mode: {R9K}");
            sb.Append($"# of Users: {Users.Count}");

            return sb.ToString();
        }
    }
}
