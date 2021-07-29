using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct KeyRegistration
        : IDisposable
        , ITransaction
        {
            public Header Header;
            public Ed25519.PublicKey VotePublicKey;
            public VrfPublicKey SelectionPublicKey;
            public ulong VoteFirst;
            public ulong VoteLast;
            public ulong VoteKeyDilution;
            NativeReference<bool> nonParticipation;

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
                Header = new Header(
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
                nonParticipation = default;
            }

            public NativeReference<bool>.ReadOnly NonParticipation => nonParticipation.AsReadOnly();

            public void SetNonParticipation(ref NativeReference<bool> value)
            {
                if (nonParticipation.IsCreated)
                    nonParticipation.Dispose();
                nonParticipation = value;
            }

            Header.ReadOnly ITransaction.Header => Header.AsReadOnly();

            public void Dispose()
            {
                Header.Dispose();
                if (nonParticipation.IsCreated)
                    nonParticipation.Dispose();
            }
        }
    }
}
