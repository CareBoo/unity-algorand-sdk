using System.Linq;
using UnityEngine;

namespace Algorand.Unity.WalletConnect
{
    public static class WalletRegistry
    {
        /// <summary>
        /// The official wallet from the Algorand Foundation.
        /// </summary>
        public static readonly AppEntry PeraWallet = new AppEntry
        {
            Id = "23138217b046ae8d9d07e62b3337fb288c4445f92f64be067809cd0a8f9454b9",
            Name = "Pera Wallet",
            HomePage = "https://perawallet.app",
            Chains = new[]
            {
                "algorand",
            },
            ImageId = "1765f9aa-f99e-414e-826b-6b570d480999",
            ImageUrl = new AppEntry.ImageUrls
            {
                Small = "https://algorand-app.s3.amazonaws.com/app-icons/Pera-walletconnect-128.png",
                Medium = "https://algorand-app.s3.amazonaws.com/app-icons/Pera-walletconnect-128.png",
                Large = "https://algorand-app.s3.amazonaws.com/app-icons/Pera-walletconnect-128.png",
            },
            App = new AppEntry.AppUrls
            {
                Browser = "",
                Ios = "https://apps.apple.com/us/app/algorand-wallet/id1459898525",
                Android = "https://play.google.com/store/apps/details?id=com.algorand.android",
                Mac = "",
                Windows = "",
                Linux = "",
            },
            Mobile = new AppEntry.AppLinkingScheme
            {
                Native = "algorand-wc:",
                Universal = "",
            },
            Desktop = new AppEntry.AppLinkingScheme
            {
                Native = "",
                Universal = "",
            },
            Metadata = new AppEntry.AppMetadata
            {
                ShortName = "Pera Wallet",
                Colors = new AppEntry.AppMetadata.AppColors
                {
                    Primary = new Color(255, 238, 85),
                    Secondary = default
                }
            },
        };

        /// <summary>
        /// All supported Algorand Wallet apps.
        /// See <see cref="SupportedWalletsForCurrentPlatform"/> if you'd like to get apps for current platform only.
        /// </summary>
        /// <returns><see cref="AppEntry[]"/> that can be used to launch apps on native device.</returns>
        public static readonly AppEntry[] SupportedWallets = new[]
        {
            PeraWallet,
        };

        /// <summary>
        /// Supported Algorand Wallet apps for the current unity platform.
        /// </summary>
        /// <returns><see cref="AppEntry[]"/> that can be used to launch apps on native device.</returns>
        public static readonly AppEntry[] SupportedWalletsForCurrentPlatform =
#if UNITY_ANDROID
            WalletRegistry.SupportedWallets
                .Where(wallet => !string.IsNullOrEmpty(wallet.App.Android))
                .ToArray();
#elif UNITY_IPHONE
            WalletRegistry.SupportedWallets
                .Where(wallet => !string.IsNullOrEmpty(wallet.App.Ios))
                .ToArray();
#elif UNITY_STANDALONE_OSX
            WalletRegistry.SupportedWallets
                .Where(wallet => !string.IsNullOrEmpty(wallet.App.Mac))
                .ToArray();
#elif UNITY_STANDALONE_WIN
            WalletRegistry.SupportedWallets
                .Where(wallet => !string.IsNullOrEmpty(wallet.App.Windows))
                .ToArray();
#elif UNITY_STANDALONE_LINUX
            WalletRegistry.SupportedWallets
                .Where(wallet => !string.IsNullOrEmpty(wallet.App.Linux))
                .ToArray();
#elif UNITY_WEBGL
            WalletRegistry.SupportedWallets
                .Where(wallet => !string.IsNullOrEmpty(wallet.App.Browser))
                .ToArray();
#else
            new AppEntry[0];
#endif
    }
}
