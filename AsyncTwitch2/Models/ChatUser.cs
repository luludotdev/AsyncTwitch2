using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTwitch.Models
{
    public struct ChatUser
    {
        /// <summary>
        /// Twitch Username.
        /// </summary>
        public string Username;

        /// <summary>
        /// Twitch Username as displayed in chat.
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// Unique identifer for this user.
        /// </summary>
        public string ID;

        /// <summary>
        /// User's chat color in RGB hex format. EG: #bf67ff
        /// </summary>
        public string Color;

        /// <summary>
        /// Whether this user has moderator status in the current room.
        /// </summary>
        public bool Moderator;

        /// <summary>
        /// Whether this user has broadcaster status in the current room.
        /// </summary>
        public bool Broadcaster;

        /// <summary>
        /// Whether this user is a subscriber in the current room.
        /// </summary>
        public bool Subscriber;

        /// <summary>
        /// Whether this user is a VIP in the current room.
        /// </summary>
        public bool VIP;

        /// <summary>
        /// All badges this user has in the current room.
        /// </summary>
        public ChatBadge[] Badges;

        public ChatUser(string _username, string _displayName, string _id, string _color, bool _moderator, bool _broadcaster, bool _subscriber, bool _vip)
        {
            Username = _username;
            DisplayName = _displayName;
            ID = _id;
            Color = _color;
            Moderator = _moderator;
            Broadcaster = _broadcaster;
            Subscriber = _subscriber;
            VIP = _vip;
            Badges = new ChatBadge[0];
        }

        public ChatUser(string _username, string _displayName, string _id, string _color, bool _moderator, bool _broadcaster, bool _subscriber, bool _vip, ChatBadge[] _badges)
        {
            Username = _username;
            DisplayName = _displayName;
            ID = _id;
            Color = _color;
            Moderator = _moderator;
            Broadcaster = _broadcaster;
            Subscriber = _subscriber;
            VIP = _vip;
            Badges = _badges;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-- Chat User --");
            sb.AppendLine($"Username: {Username}");
            sb.AppendLine($"Display Name: {DisplayName}");
            sb.AppendLine($"User ID: {ID}");
            sb.AppendLine($"Chat Color: {Color}");
            sb.AppendLine($"Is Moderator: {Moderator}");
            sb.AppendLine($"Is Broadcaster: {Broadcaster}");
            sb.AppendLine($"Is Subscriber: #{Subscriber}");
            sb.AppendLine($"Is VIP: #{VIP}");
            sb.AppendLine($"# of Badges: #{Badges.Length}");

            return sb.ToString();
        }
    }
}
