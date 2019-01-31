namespace AsyncTwitch.Models
{
    public struct RawMessage
    {
        public string Hostname;
        public string Type;
        public string ChannelName;
        public string Message;

        public RawMessage(string _hostname, string _type, string _channelName, string _message)
        {
            Hostname = _hostname;
            Type = _type;
            ChannelName = _channelName;
            Message = _message;
        }
    }
}
