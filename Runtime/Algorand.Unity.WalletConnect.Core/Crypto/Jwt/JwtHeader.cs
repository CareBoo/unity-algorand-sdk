using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Algorand.Unity.WalletConnect.Core
{
    [Serializable]
    [AlgoApiObject]
    public partial struct JwtHeader
    {
        [FormerlySerializedAs("signingAlgorithm")]
        [SerializeField]
        private string alg;

        [FormerlySerializedAs("tokenType")]
        [SerializeField]
        private string typ;

        /// <summary>
        ///     The signing algorithm used for this JWT.
        /// </summary>
        [AlgoApiField("alg")]
        public string SigningAlgorithm
        {
            get => alg;
            set => alg = value;
        }

        /// <summary>
        ///     Type of this token, usually "JWT".
        /// </summary>
        [AlgoApiField("typ")]
        public string TokenType
        {
            get => typ;
            set => typ = value;
        }
    }
}