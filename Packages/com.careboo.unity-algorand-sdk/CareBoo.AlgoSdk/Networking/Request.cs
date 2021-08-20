using UnityEngine.Networking;

namespace AlgoSdk
{
    public readonly ref struct Request
    {
        readonly UnityWebRequest unityWebRequest;

        private Request(string method, string url)
        {
            unityWebRequest = new UnityWebRequest()
            {
                url = url,
                method = method,
                downloadHandler = new DownloadHandlerBuffer()
            };
        }

        public void SetRequestHeader(string key, string value)
        {
            unityWebRequest.SetRequestHeader(key, value);
        }

        public void Dispose()
        {
            unityWebRequest.Dispose();
        }
    }
}
