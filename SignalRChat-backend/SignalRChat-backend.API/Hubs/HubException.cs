using System.Runtime.Serialization;

namespace SignalRChat_backend.API.Hubs
{
    [Serializable]
    public class HubException : Exception
    {
        public HubException() { }
        public HubException(string message)
            : base(message) { }
        public HubException(string message, Exception inner)
            : base(message, inner) { }
        protected HubException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
