using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public interface ITransaction
    {
        ulong Fee { get; set; }
        ulong FirstValidRound { get; set; }
        Sha512_256_Hash GenesisHash { get; set; }
        ulong LastValidRound { get; set; }
        Address Sender { get; set; }
        TransactionType TransactionType { get; }

        NativeText GenesisId { get; set; }
        NativeReference<Address> Group { get; set; }
        NativeReference<Address> Lease { get; set; }
        NativeText Note { get; set; }
        NativeReference<Address> RekeyTo { get; set; }
    }

    public enum TransactionType : ushort
    {
        None,
        Payment,
        KeyRegistration,
        AssetTransfer,
        AssetFreeze,
        AssetConfiguration,
        ApplicationCall,
        Count,
    }

    public static partial class Transaction
    {
        public static SignedTransaction<Signature, TTransaction> Sign<TTransaction>(
            this ref TTransaction transaction, in PrivateKey privateKey
            )
            where TTransaction : unmanaged, ITransaction, IDisposable
        {
            var signature = new Signature();
            return new SignedTransaction<Signature, TTransaction>(in signature, ref transaction);
        }
    }
}
