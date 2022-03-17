using System;
using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Contains information about an app that supports WalletConnect
    /// </summary>
    [Serializable]
    public class AppEntry
    {
        public string Id;
        public string Name;
        public string HomePage;
        public string[] Chains;
        public string ImageId;
        public ImageUrls ImageUrl;
        public AppUrls App;
        public AppLinkingScheme Mobile;
        public AppLinkingScheme Desktop;
        public AppMetadata Metadata;

        public void LaunchForConnect(HandshakeUrl handshake) =>
            LaunchApp(handshake.Url);

        public void LaunchForSigning(string walletConnectVersion = "1") =>
            LaunchApp($"wc:00e46b69-d0cc-4b3e-b6a2-cee442f97188@{walletConnectVersion}");

        void LaunchApp(string url)
        {
            var deepLinkUrl = FormatUrlForDeepLink(url);
            UnityEngine.Application.OpenURL(deepLinkUrl);
        }

        string FormatUrlForDeepLink(string url)
        {
#if UNITY_IPHONE
            if (!string.IsNullOrEmpty(Mobile.Universal))
            {
                return $"{Mobile.Universal}/wc?uri={url}";
            }
            else if (!string.IsNullOrEmpty(Mobile.Native))
            {
                return $"{Mobile.Native}//wc?uri={url}";
            }
#endif
            return url;
        }

        [Serializable]
        public struct ImageUrls
        {
            public string Small;
            public string Medium;
            public string Large;
        }

        [Serializable]
        public struct AppUrls
        {
            public string Browser;
            public string Ios;
            public string Android;
            public string Mac;
            public string Windows;
            public string Linux;
        }

        [Serializable]
        public struct AppLinkingScheme
        {
            public string Native;
            public string Universal;
        }

        [Serializable]
        public struct AppMetadata
        {
            public string ShortName;
            public AppColors Colors;

            public struct AppColors
            {
                public Color Primary;
                public Color Secondary;
            }
        }
    }
}
