using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A response from <see cref="IAlgodClient.StartCatchup"/>
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public struct CatchupMessage
        : IEquatable<CatchupMessage>
    {
        /// <summary>
        /// Catchup start response string
        /// </summary>
        [AlgoApiField("catchup-message", null)]
        [Tooltip("Catchup start response string")]
        public FixedString512Bytes Message;

        public bool Equals(CatchupMessage other)
        {
            return Message.Equals(other.Message);
        }
    }
}
