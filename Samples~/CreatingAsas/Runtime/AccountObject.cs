using System;
using AlgoSdk;
using UnityEngine;

[CreateAssetMenu]
public class AccountObject
    : ScriptableObject
    , IAccount
    , ISigner
    , ISerializationCallbackReceiver
{
    [SerializeField]
    Mnemonic mnemonic;

    [SerializeField]
    Address address;

    Account account;

    public Address Address => account.Address;

    public SignedTxn<T> SignTxn<T>(T txn)
        where T : ITransaction, IEquatable<T> => account.SignTxn(txn);

    public SignedTxn<T>[] SignTxns<T>(T[] txns, TxnIndices txnsToSign)
        where T : ITransaction, IEquatable<T> => account.SignTxns(txns, txnsToSign);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [ContextMenu(nameof(GenerateNewAccount))]
    public void GenerateNewAccount()
    {
        account = Account.GenerateAccount();
        Debug.Log($"Generated account with address: {account.Address}");
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        var (privateKey, publicKey) = account;
        address = publicKey;
        mnemonic = privateKey.ToMnemonic();
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        account = new Account(mnemonic.ToPrivateKey());
    }
}
