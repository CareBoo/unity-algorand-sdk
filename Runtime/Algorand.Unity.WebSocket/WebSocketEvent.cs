using System;

namespace Algorand.Unity.WebSocket
{
    public class WebSocketEvent
    {
        public enum WebSocketEventType
        {
            Nothing,
            Open,
            Close,
            Payload,
            Error,
        }

        public WebSocketEventType Type;
        public ulong ClientId;
        public byte[] Payload;
        public string Error;
        public string Reason;
    }
}
