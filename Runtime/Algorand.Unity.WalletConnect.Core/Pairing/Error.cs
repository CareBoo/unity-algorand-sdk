using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct Error : IEquatable<Error>
    {
        [SerializeField]
        private int code;

        [SerializeField]
        private string message;

        [AlgoApiField("code")]
        public int Code
        {
            get => code;
            set => code = value;
        }

        [AlgoApiField("message")]
        public string Message
        {
            get => message;
            set => message = value;
        }

        public bool Equals(Error other)
        {
            return Code.Equals(other.Code)
                   && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
