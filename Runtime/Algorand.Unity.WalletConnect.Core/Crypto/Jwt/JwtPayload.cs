using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [Serializable]
    [AlgoApiObject]
    public partial struct JwtPayload
    {
        [SerializeField]
        private string iss;

        [SerializeField]
        private string sub;

        [SerializeField]
        private string aud;

        [SerializeField]
        private long iat;

        [SerializeField]
        private long exp;

        [AlgoApiField("iss")]
        public string Issuer
        {
            get => iss;
            set => iss = value;
        }

        [AlgoApiField("sub")]
        public string Subject
        {
            get => sub;
            set => sub = value;
        }

        [AlgoApiField("aud")]
        public string Audience
        {
            get => aud;
            set => aud = value;
        }

        [AlgoApiField("iat")]
        public long IssuedAt
        {
            get => iat;
            set => iat = value;
        }

        [AlgoApiField("exp")]
        public long Expiration
        {
            get => exp;
            set => exp = value;
        }
    }
}