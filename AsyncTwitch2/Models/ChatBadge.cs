using System.Text;

namespace AsyncTwitch.Models
{
    public struct ChatBadge
    {
        /// <summary>
        /// Badge Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Badge Version
        /// </summary>
        public short Version;

        public ChatBadge(string _name, short _version)
        {
            Name = _name;
            Version = _version;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-- Twitch Chat Badge --");
            sb.AppendLine($"Name: {Name}");
            sb.Append($"Version: {Version}");

            return sb.ToString();
        }
    }
}
