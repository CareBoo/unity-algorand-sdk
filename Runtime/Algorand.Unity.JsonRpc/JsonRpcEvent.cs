namespace Algorand.Unity.JsonRpc
{
    public struct JsonRpcEvent
    {
        public JsonRpcEventType Type { get; set; }
        public JsonRpcRequest Request { get; set; }
        public JsonRpcResponse Response { get; set; }
        public string Error { get; set; }
        public string Reason { get; set; }
    }
}
