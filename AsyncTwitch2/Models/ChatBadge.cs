using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Version;

        public ChatBadge(string _name, int _version)
        {
            Name = _name;
            Version = _version;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-- Twitch Chat Badge --");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Version: {Version}");

            return sb.ToString();
        }
    }
}
