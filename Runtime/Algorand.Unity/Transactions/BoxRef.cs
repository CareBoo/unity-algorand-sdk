using System;
using System.Text;
using UnityEngine;

namespace Algorand.Unity
{
    [Serializable]
    [AlgoApiObject]
    public partial struct BoxRef : IEquatable<BoxRef>
    {
        [SerializeField]
        [Tooltip("[i] Index of the foreign application that contains this box.")]
        private ulong index;

        [SerializeField]
        [Tooltip("[n] The name key of the box.")]
        private string name;

        /// <summary>
        /// [i] Index of the foreign application that contains this box.
        /// </summary>
        [AlgoApiField("i")]
        public ulong Index
        {
            get => index;
            set => index = value;
        }

        /// <summary>
        /// [n] The name key of the box.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        [AlgoApiField("n")]
        public byte[] NameBytes
        {
            get => Encoding.UTF8.GetBytes(name);
            set => name = Encoding.UTF8.GetString(value);
        }

        public bool Equals(BoxRef other)
        {
            return index.Equals(other.index)
                   && StringComparer.Equals(name, other.name);
        }
    }
}
