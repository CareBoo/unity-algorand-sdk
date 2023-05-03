using System;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// A description for each of the methods in the contract.
    /// </summary>
    /// <remarks>
    /// See <see href="https://github.com/algorandfoundation/ARCs/blob/2723ca7f4568c0d19c412198651404cb0a0e9dbd/ARCs/arc-0004.md#contract-description">ARC-0004</see>
    /// for details.
    /// </remarks>
    [AlgoApiObject, Serializable]
    public partial struct Contract
        : IEquatable<Contract>
    {
        [SerializeField, Tooltip("A user-friendly name for the contract")]
        private string name;

        [SerializeField, Tooltip("Optional, user-friendly description for the interface")]
        private string description;

        [SerializeField, Tooltip("Optional object listing the contract instances across different networks")]
        private Deployments networks;

        [SerializeField, Tooltip("All of the methods that the contract implements")]
        private Method[] methods;

        /// <summary>
        /// A user-friendly name for the contract
        /// </summary>
        [AlgoApiField("name")]
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Optional, user-friendly description for the interface
        /// </summary>
        [AlgoApiField("desc")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        /// <summary>
        /// Optional object listing the contract instances across different networks
        /// </summary>
        [AlgoApiField("networks")]
        public Deployments Networks
        {
            get => networks;
            set => networks = value;
        }

        /// <summary>
        /// All of the methods that the contract implements
        /// </summary>
        [AlgoApiField("methods")]
        public Method[] Methods
        {
            get => methods;
            set => methods = value;
        }

        public bool Equals(Contract other)
        {
            return StringComparer.Equals(Name, other.Name)
                && StringComparer.Equals(Description, other.Description)
                && Networks.Equals(other.Networks)
                && ArrayComparer.Equals(Methods, other.Methods)
                ;
        }

        /// <summary>
        /// Represents information about the deployments of a contract across different networks.
        /// </summary>
        [AlgoApiFormatter(typeof(Deployments.Formatter)), Serializable]
        public partial struct Deployments
            : IEquatable<Deployments>
        {
            [SerializeField, Tooltip("Contains information about the deployed contract in the network indicated.")]
            private Deployment[] deployments;

            public Deployment this[int index]
            {
                get => deployments[index];
                set => deployments[index] = value;
            }

            public int Length => deployments?.Length ?? 0;

            public long LongLength => deployments?.LongLength ?? 0L;

            public static implicit operator Deployment[](Deployments d)
            {
                return d.deployments;
            }

            public static implicit operator Deployments(Deployment[] d)
            {
                return new Deployments { deployments = d };
            }

            public Deployment[] AsArray() => deployments;

            public bool Equals(Deployments other)
            {
                return ArrayComparer.Equals(deployments, other.deployments);
            }

            public sealed class Formatter : IAlgoApiFormatter<Deployments>
            {
                public const string AppIdHeader = "appID";

                public static readonly FixedString32Bytes AppIdHeaderFixed = AppIdHeader;

                public Deployments Deserialize(ref JsonReader reader)
                {
                    if (!reader.TryRead(JsonToken.ObjectBegin))
                        JsonReadError.IncorrectType.ThrowIfError(reader);
                    using var deployments = new NativeList<Deployment>(Allocator.Temp);
                    while (reader.Peek() != JsonToken.ObjectEnd
                        && reader.Peek() != JsonToken.None)
                    {
                        FixedString64Bytes network = default;
                        reader.ReadString(ref network).ThrowIfError(reader);

                        if (!reader.TryRead(JsonToken.ObjectBegin))
                            JsonReadError.IncorrectType.ThrowIfError(reader);
                        FixedString32Bytes appIdHeader = default;
                        reader.ReadString(ref appIdHeader).ThrowIfError(reader);
                        if (appIdHeader != AppIdHeaderFixed)
                            JsonReadError.IncorrectType.ThrowIfError(reader);
                        reader.ReadNumber(out ulong appId).ThrowIfError(reader);
                        if (!reader.TryRead(JsonToken.ObjectEnd))
                            JsonReadError.IncorrectType.ThrowIfError(reader);

                        deployments.Add(
                            new Deployment
                            {
                                Network = network,
                                AppId = appId
                            }
                        );
                    }
                    if (!reader.TryRead(JsonToken.ObjectEnd))
                        JsonReadError.IncorrectFormat.ThrowIfError(reader);
                    return new Deployments { deployments = deployments.AsArray().ToArray() };
                }

                public Deployments Deserialize(ref MessagePackReader reader)
                {
                    var length = reader.ReadMapHeader();
                    var deployments = new Deployment[length];
                    for (var i = 0; i < length; i++)
                    {
                        FixedString64Bytes network = default;
                        reader.ReadString(ref network);
                        var appIdObjLength = reader.ReadMapHeader();
                        if (appIdObjLength != 1)
                            throw new SerializationException($"Was expecting only 1 entry in appId Obj, but was {appIdObjLength}");
                        var appId = reader.ReadUInt64();
                        deployments[i] = new Deployment
                        {
                            Network = network,
                            AppId = appId
                        };
                    }
                    return new Deployments { deployments = deployments };
                }

                public void Serialize(ref JsonWriter writer, Deployments value)
                {
                    writer.BeginObject();
                    for (var i = 0; i < value.Length; i++)
                    {
                        if (i > 0)
                            writer.BeginNextItem();
                        var (network, appId) = value[i];
                        writer.WriteObjectKey(network);

                        writer.BeginObject();
                        writer.WriteObjectKey(AppIdHeaderFixed);
                        writer.WriteNumber(appId);
                        writer.EndObject();
                    }
                    writer.EndObject();
                }

                public void Serialize(ref MessagePackWriter writer, Deployments value)
                {
                    writer.WriteMapHeader(value.Length);
                    for (var i = 0; i < value.Length; i++)
                    {
                        var (network, appId) = value[i];
                        writer.WriteString(network);
                        writer.WriteMapHeader(1);
                        writer.WriteString(AppIdHeaderFixed);
                        writer.WriteBigEndian(appId);
                    }
                }
            }
        }

        /// <summary>
        /// A key-value representing a contract deployment in a network.
        /// </summary>
        [Serializable]
        public struct Deployment
            : IEquatable<Deployment>
        {
            [SerializeField, Tooltip("The base64 genesis hash of the network.")]
            private FixedString64Bytes network;

            [SerializeField, Tooltip("The app ID of the deployed contract in this network.")]
            private AppIndex appId;

            /// <summary>
            /// The base64 genesis hash of the network.
            /// </summary>
            public FixedString64Bytes Network
            {
                get => network;
                set => network = value;
            }

            /// <summary>
            /// The app ID of the deployed contract in this network.
            /// </summary>
            public AppIndex AppId
            {
                get => appId;
                set => appId = value;
            }

            public void Deconstruct(out FixedString64Bytes network, out AppIndex appId)
            {
                network = this.network;
                appId = this.appId;
            }

            public bool Equals(Deployment other)
            {
                return Network.Equals(other.Network)
                    && AppId.Equals(other.AppId)
                    ;
            }
        }
    }
}
