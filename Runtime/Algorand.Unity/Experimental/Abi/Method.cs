using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// A method description provides further information about a method beyond its signature.
    /// </summary>
    /// <remarks>
    /// See <see href="https://github.com/algorandfoundation/ARCs/blob/2723ca7f4568c0d19c412198651404cb0a0e9dbd/ARCs/arc-0004.md#method-description">ARC-0004</see>
    /// for more details.
    /// </remarks>
    [AlgoApiObject, Serializable]
    public partial struct Method
        : IEquatable<Method>
    {
        [SerializeField, Tooltip("The name of the method.")]
        private string name;

        [SerializeField, Tooltip("Optional, user-friendly description for the method.")]
        private string description;

        [SerializeField, Tooltip("The arguments of the method, in order.")]
        private Arg[] arguments;

        [SerializeField, Tooltip("Information about the method's return value.")]
        private Return returns;

        /// <summary>
        /// The name of the method.
        /// </summary>
        [AlgoApiField("name")]
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Optional, user-friendly description for the method.
        /// </summary>
        [AlgoApiField("desc")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        /// <summary>
        /// The arguments of the method, in order.
        /// </summary>
        [AlgoApiField("args")]
        public Arg[] Arguments
        {
            get => arguments;
            set => arguments = value;
        }

        /// <summary>
        /// Information about the method's return value.
        /// </summary>
        [AlgoApiField("returns")]
        public Return Returns
        {
            get => returns;
            set => returns = value;
        }

        public bool Equals(Method other)
        {
            return StringComparer.Equals(Name, other.Name)
                && StringComparer.Equals(Description, other.Description)
                && ArrayComparer.Equals(Arguments, other.Arguments)
                && Returns.Equals(other.Returns)
                ;
        }

        public NativeText GetSignature(Allocator allocator)
        {
            var text = new NativeText(allocator);
            try
            {
                text.Append(Name);
                text.Append(new Unicode.Rune('('));
                var argLength = Arguments?.Length ?? 0;
                for (var i = 0; i < argLength; i++)
                {
                    if (i > 0)
                        text.Append(new Unicode.Rune(','));
                    text.Append(Arguments[i].Type.Name);
                }
                text.Append(new Unicode.Rune(')'));
                text.Append(Returns.Type?.Name ?? "void");
            }
            catch (Exception ex)
            {
                text.Dispose();
                throw ex;
            }
            return text;
        }

        /// <summary>
        /// Represents a <see cref="Method"/> argument.
        /// </summary>
        [AlgoApiObject, Serializable]
        public partial struct Arg
            : IEquatable<Arg>
        {
            [SerializeField, SerializeReference, Tooltip("The type of the argument.")]
            private IAbiType type;

            [SerializeField, Tooltip("Optional, user-friendly name for the argument.")]
            private string name;

            [SerializeField, Tooltip("Optional, user-friendly description for the argument.")]
            private string description;

            /// <summary>
            /// The type of the argument.
            /// </summary>
            [AlgoApiField("type")]
            public IAbiType Type
            {
                get => type;
                set => type = value;
            }

            /// <summary>
            /// Optional, user-friendly name for the argument.
            /// </summary>
            [AlgoApiField("name")]
            public string Name
            {
                get => name;
                set => name = value;
            }

            /// <summary>
            /// Optional, user-friendly description for the argument.
            /// </summary>
            [AlgoApiField("desc")]
            public string Description
            {
                get => description;
                set => description = value;
            }

            public bool Equals(Arg other)
            {
                return IAbiTypeComparer.Instance.Equals(Type, other.Type)
                    && StringComparer.Equals(Name, other.Name)
                    && StringComparer.Equals(Description, other.Description)
                    ;
            }
        }

        /// <summary>
        /// Represents a <see cref="Method"/> return value.
        /// </summary>
        [AlgoApiObject, Serializable]
        public partial struct Return
            : IEquatable<Return>
        {
            [SerializeField, SerializeReference, Tooltip("The type of the return value, or \"void\" to indicate no return value.")]
            private IAbiType type;

            [SerializeField, Tooltip("Optional, user-friendly description for the return value.")]
            private string description;

            /// <summary>
            /// The type of the return value, or \"void\" to indicate no return value. 
            /// </summary>
            [AlgoApiField("type")]
            public IAbiType Type
            {
                get => type;
                set => type = value;
            }

            /// <summary>
            /// Optional, user-friendly description for the return value.
            /// </summary>
            [AlgoApiField("desc")]
            public string Description
            {
                get => description;
                set => description = value;
            }

            public bool Equals(Return other)
            {
                return IAbiTypeComparer.Instance.Equals(Type, other.Type)
                    && StringComparer.Equals(Description, other.Description)
                    ;
            }
        }
    }
}
