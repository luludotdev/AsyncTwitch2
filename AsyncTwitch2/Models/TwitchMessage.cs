using System;
using System.Text;

namespace AsyncTwitch.Models
{
    public struct TwitchMessage
    {
        /// <summary>
        /// The content of the Twitch Message.
        /// </summary>
        public string Content;

        /// <summary>
        /// User who sent this message.
        /// </summary>
        public ChatUser Author;

        /// <summary>
        /// Is this message a cheer.
        /// </summary>
        public bool Cheer;

        /// <summary>
        /// Amount cheered in this message. Defaults to -1 if the message is not a cheer.
        /// </summary>
        public int CheerAmount;

        /// <summary>
        /// Array of emotes in this message.
        /// </summary>
        public Emote[] Emotes;

        /// <summary>
        /// Unique identifier for this message.
        /// </summary>
        public string ID;

        /// <summary>
        /// Timestamp for when this message was sent.
        /// </summary>
        public DateTime Timestamp;

        /// <summary>
        /// Room state info for the channel in which this message was sent.
        /// </summary>
        public RoomState Room;

        /// <summary>
        /// Raw IRC message text.
        /// </summary>
        public string RawMessage;

        public TwitchMessage(string _content, ChatUser _author, bool _cheer, int _cheerAmount, Emote[] _emotes, string _id, DateTime _timestamp, RoomState _room, string _rawMessage)
        {
            Content = _content;
            Author = _author;
            Cheer = _cheer;
            CheerAmount = _cheerAmount;
            Emotes = _emotes;
            ID = _id;
            Timestamp = _timestamp;
            Room = _room;
            RawMessage = _rawMessage;
        }

        public TwitchMessage(string _content, string _rawMessage)
        {
            Content = _content;
            Author = new ChatUser();
            Cheer = false;
            CheerAmount = -1;
            Emotes = new Emote[0];
            ID = "";
            Timestamp = DateTime.UtcNow;
            Room = new RoomState();
            RawMessage = _rawMessage;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-- Twitch Message --");
            sb.AppendLine($"Content: {Content}");
            sb.AppendLine($"Author: {Author.DisplayName}");
            sb.AppendLine($"Is Cheer: {Cheer}");
            sb.AppendLine($"Cheer Amount: {CheerAmount}");
            sb.AppendLine($"# of Emotes: {Emotes.Length}");
            sb.AppendLine($"Message ID: {ID}");
            sb.AppendLine($"Timestamp: {Timestamp}");
            sb.Append($"Room: {Room.Name}");

            return sb.ToString();
        }
    }
}
