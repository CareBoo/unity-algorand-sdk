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
