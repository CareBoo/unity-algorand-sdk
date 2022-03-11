using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Contains information about an app that supports WalletConnect
    /// </summary>
    public struct AppEntry
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

        public struct ImageUrls
        {
            public string Small;
            public string Medium;
            public string Large;
        }

        public struct AppUrls
        {
            public string Browser;
            public string Ios;
            public string Android;
            public string Mac;
            public string Windows;
            public string Linux;
        }

        public struct AppLinkingScheme
        {
            public string Native;
            public string Universal;
        }

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
