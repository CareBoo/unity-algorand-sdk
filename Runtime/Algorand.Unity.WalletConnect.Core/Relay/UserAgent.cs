using System;

namespace Algorand.Unity.WalletConnect.Core
{
    [Serializable]
    public partial struct UserAgent
    {
        public string protocol;
        public string version;
        public string sdkVersion;

        public static UserAgent Default => new()
        {
            protocol = "wc",
            version = "2",
            sdkVersion = "4.2.0-exp.1"
        };

        public override readonly string ToString()
        {
            var os = Environment.OSVersion.Platform.ToString();
            var osVersion = Environment.OSVersion.Version.ToString();

            var environment = $"WalletConnectSharpv2:{Environment.Version}";
            var sdkType = "unity-algorand";

            return $"{protocol}-{version}/{sdkType}-{sdkVersion}/{os}-{osVersion}/{environment}";
        }
    }
}
