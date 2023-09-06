using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct Metadata : IEquatable<Metadata>
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private string description;

        [SerializeField]
        private string url;

        [SerializeField]
        private string[] icons;

        [SerializeField]
        private string verifyUrl;

        [SerializeField]
        private Optional<RedirectData> redirect;

        [AlgoApiField("name")]
        public string Name
        {
            get => name;
            set => name = value;
        }

        [AlgoApiField("description")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        [AlgoApiField("url")]
        public string Url
        {
            get => url;
            set => url = value;
        }

        [AlgoApiField("icons")]
        public string[] Icons
        {
            get => icons;
            set => icons = value;
        }

        [AlgoApiField("verifyUrl")]
        public string VerifyUrl
        {
            get => verifyUrl;
            set => verifyUrl = value;
        }

        [AlgoApiField("redirect")]
        public Optional<RedirectData> Redirect
        {
            get => redirect;
            set => redirect = value;
        }

        public bool Equals(Metadata other)
        {
            return StringComparer.Equals(name, other.name)
                   && StringComparer.Equals(url, other.url)
                ;
        }

        [AlgoApiObject]
        [Serializable]
        public partial struct RedirectData : IEquatable<RedirectData>
        {
            [SerializeField]
            private string native;

            [SerializeField]
            private string universal;

            [AlgoApiField("native")]
            public string Native
            {
                get => native;
                set => native = value;
            }

            [AlgoApiField("universal")]
            public string Universal
            {
                get => universal;
                set => universal = value;
            }

            public bool Equals(RedirectData other)
            {
                return string.Equals(native, other.native)
                       && string.Equals(universal, other.universal)
                    ;
            }
        }
    }
}
