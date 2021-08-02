using System.Runtime.InteropServices;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    // [BurstCompatible]
    // public struct PaymentTransaction
    //     : ITransaction
    // {
    //     MessagePackTransaction data;

    //     public MessagePackTransaction RawData => data;

    //     public int NumFields
    //     {
    //         get
    //         {
    //             int count = data.NumFields + 2;
    //             if (CloseRemainderTo != default) count++;
    //             return count;
    //         }
    //     }

    //     #region Header
    //     public ulong Fee { get => data.Fee; set => data.Fee = value; }
    //     public ulong FirstValidRound { get => data.FirstValidRound; set => data.FirstValidRound = value; }
    //     public Sha512_256_Hash GenesisHash { get => data.GenesisHash; set => data.GenesisHash = value; }
    //     public ulong LastValidRound { get => data.LastValidRound; set => data.LastValidRound = value; }
    //     public Address Sender { get => data.Sender; set => data.Sender = value; }
    //     public TransactionType TransactionType { get => data.TransactionType; }
    //     public FixedString32 GenesisId { get => data.GenesisId; set => data.GenesisId = value; }
    //     public Address Group { get => data.Group; set => data.Group = value; }
    //     public Address Lease { get => data.Lease; set => data.Lease = value; }
    //     public FixedString4096 Note { get => data.Note; set => data.Note = value; }
    //     public Address RekeyTo { get => data.RekeyTo; set => data.RekeyTo = value; }
    //     #endregion // Header

    //     public Address Receiver { get => data.Receiver; set => data.Receiver = value; }
    //     public ulong Amount { get => data.Amount; set => data.Amount = value; }
    //     public Address CloseRemainderTo { get => data.CloseRemainderTo; set => data.CloseRemainderTo = value; }

    //     public PaymentTransaction(
    //         in ulong fee,
    //         in ulong firstValidRound,
    //         in Sha512_256_Hash genesisHash,
    //         in ulong lastValidRound,
    //         in Address sender,
    //         in Address receiver,
    //         in ulong amount
    //     )
    //     {
    //         data = new UnsafeTransaction()
    //         {
    //             Fee = fee,
    //             FirstValidRound = firstValidRound,
    //             GenesisHash = genesisHash,
    //             LastValidRound = lastValidRound,
    //             TransactionType = TransactionType.Payment,
    //             Sender = sender,
    //             Receiver = receiver,
    //             Amount = amount
    //         };
    //     }

    //     internal PaymentTransaction(
    //         in UnsafeTransaction rawData
    //     )
    //     {
    //         data = rawData;
    //     }
    // }
}
