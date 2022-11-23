using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Algorand.Unity.WalletConnect
{
    /// <summary>
    /// Contains information about an app that supports WalletConnect
    /// </summary>
    [Serializable]
    public class AppEntry
    {
        /// <summary>
        /// Unique id of this app
        /// </summary>
        public string Id;

        /// <summary>
        /// Name of this app.
        /// </summary>
        public string Name;

        /// <summary>
        /// Homepage for this app.
        /// </summary>
        public string HomePage;

        /// <summary>
        /// This App's supported blockchains
        /// </summary>
        public string[] Chains;

        /// <summary>
        /// A unique id for this App's image, useful for caching.
        /// </summary>
        public string ImageId;

        /// <summary>
        /// ImageUrls for downloading this App's image.
        /// </summary>
        public ImageUrls ImageUrl;

        /// <summary>
        /// The locations of this app on available platforms.
        /// </summary>
        public AppUrls App;

        /// <summary>
        /// Contains information about ios native and universal linking.
        /// </summary>
        public AppLinkingScheme Mobile;

        /// <summary>
        /// Contains information about native and universal linking on desktop.
        /// </summary>
        public AppLinkingScheme Desktop;

        /// <summary>
        /// Additional Metadata that can be useful when showing the app.
        /// </summary>
        public AppMetadata Metadata;

        /// <summary>
        /// Launch this app natively to approve a WalletConnect session.
        /// </summary>
        /// <param name="handshake">The handshake used for the session.</param>
        public void LaunchForConnect(HandshakeUrl handshake) =>
            LaunchApp(handshake.Url);

        /// <summary>
        /// Launch this app natively to allow users to approve a signing request from a
        /// WalletConnect session.
        /// </summary>
        /// <param name="walletConnectVersion">The WalletConnect version of your session.</param>
        public void LaunchForSigning(string walletConnectVersion = "1") =>
            LaunchApp($"wc:00e46b69-d0cc-4b3e-b6a2-cee442f97188@{walletConnectVersion}");

        private void LaunchApp(string url)
        {
            var deepLinkUrl = FormatUrlForDeepLink(url);
            UnityEngine.Application.OpenURL(deepLinkUrl);
        }

        public string FormatUrlForDeepLink(string url)
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            return FormatUrlForDeepLinkIos(url);
#else
            return url;
#endif
        }

        public string FormatUrlForDeepLinkIos(string url)
        {
            url = UnityWebRequest.EscapeURL(url);
            if (!string.IsNullOrEmpty(Mobile.Universal))
            {
                return $"{Mobile.Universal}/wc?uri={url}";
            }
            else if (!string.IsNullOrEmpty(Mobile.Native))
            {
                return $"{Mobile.Native}//wc?uri={url}";
            }
            else
            {
                return "";
            }
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
            /// <summary>
            /// The prefix used when using a native link.
            /// </summary>
            public string Native;

            /// <summary>
            /// The url used when using a universal link.
            /// </summary>
            public string Universal;
        }

        [Serializable]
        public struct AppMetadata
        {
            /// <summary>
            /// A short name of this app if it has one -- useful on smaller screens.
            /// </summary>
            public string ShortName;

            /// <summary>
            /// Primary and secondary colors of this App.
            /// </summary>
            public AppColors Colors;

            public struct AppColors
            {
                public Color Primary;
                public Color Secondary;
            }
        }
    }
}
