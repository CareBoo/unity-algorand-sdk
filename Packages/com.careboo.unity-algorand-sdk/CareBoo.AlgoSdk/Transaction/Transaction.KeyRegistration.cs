using System;
using AlgoSdk.Crypto;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        [MessagePackObject]
        public struct KeyRegistration
        : IDisposable
        , ITransaction
        {
            Header header;

            [Key("votekey")]
            public Ed25519.PublicKey VotePublicKey;

            [Key("selkey")]
            public VrfPublicKey SelectionPublicKey;

            [Key("votefst")]
            public ulong VoteFirst;

            [Key("votelst")]
            public ulong VoteLast;

            [Key("votekd")]
            public ulong VoteKeyDilution;

            [Key("nonpart")]
            public NativeReference<bool> NonParticipation;

            [Key("fee")]
            public ulong Fee { get => header.Fee; set => header.Fee = value; }

            [Key("fv")]
            public ulong FirstValidRound { get => header.FirstValidRound; set => header.FirstValidRound = value; }

            [Key("gh")]
            public Sha512_256_Hash GenesisHash { get => header.GenesisHash; set => header.GenesisHash = value; }

            [Key("lv")]
            public ulong LastValidRound { get => header.LastValidRound; set => header.LastValidRound = value; }

            [Key("snd")]
            public Address Sender { get => header.Sender; set => header.Sender = value; }

            [Key("type")]
            public TransactionType TransactionType => header.TransactionType;

            [Key("gen")]
            public NativeText GenesisId { get => header.GenesisId; set => header.GenesisId = value; }

            [Key("grp")]
            public NativeReference<Address> Group { get => header.Group; set => header.Group = value; }

            [Key("lx")]
            public NativeReference<Address> Lease { get => header.Lease; set => header.Lease = value; }

            [Key("note")]
            public NativeText Note { get => header.Note; set => header.Note = value; }

            [Key("rekey")]
            public NativeReference<Address> RekeyTo { get => header.RekeyTo; set => header.RekeyTo = value; }

            public KeyRegistration(
                in ulong fee,
                in ulong firstValidRound,
                in Sha512_256_Hash genesisHash,
                in ulong lastValidRound,
                in Address sender,
                in Ed25519.PublicKey votePublicKey,
                in VrfPublicKey selectionPublicKey,
                in ulong voteFirst,
                in ulong voteLast,
                in ulong voteKeyDilution
            )
            {
                header = new Header(
                    in fee,
                    in firstValidRound,
                    in genesisHash,
                    in lastValidRound,
                    in sender,
                    TransactionType.KeyRegistration
                );

                VotePublicKey = votePublicKey;
                SelectionPublicKey = selectionPublicKey;
                VoteFirst = voteFirst;
                VoteLast = voteLast;
                VoteKeyDilution = voteKeyDilution;
                NonParticipation = default;
            }

            public void Dispose()
            {
                header.Dispose();
                if (NonParticipation.IsCreated)
                    NonParticipation.Dispose();
            }
        }
    }
}
