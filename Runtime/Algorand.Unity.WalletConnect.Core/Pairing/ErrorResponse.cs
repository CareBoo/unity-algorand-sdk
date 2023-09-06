using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct ErrorResponse : IEquatable<ErrorResponse>
    {
        [SerializeField]
        private int id;

        [SerializeField]
        private Error error;

        [AlgoApiField("id")]
        public int Id
        {
            get => id;
            set => id = value;
        }

        [AlgoApiField("error")]
        public Error Error
        {
            get => error;
            set => error = value;
        }

        public bool Equals(ErrorResponse other)
        {
            return Id.Equals(other.Id)
                && Error.Equals(other.Error)
                ;
        }
    }
}
