﻿using System.Runtime.Serialization;

namespace SignalRChat_backend.Services.Services
{
    [Serializable]
    public class UserException : Exception
    {
        public UserException() { }
        public UserException(string message) 
            : base(message){ }
        public UserException(string message, Exception inner)
            : base(message, inner) { }
        protected UserException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
