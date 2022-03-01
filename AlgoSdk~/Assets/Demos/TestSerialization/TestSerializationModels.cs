using UnityEngine;
using AlgoSdk;
using AlgoSdk.Crypto;

public class TestSerializationModels : MonoBehaviour
{
    public Mnemonic mnemonic;
    public Address address;
    public Optional<bool> optionalBool;
    public PrivateKey privateKey;
    public Ed25519.PublicKey publicKey;
    public Ed25519.Seed seed;
    public AccountInfo accountInfo;
    public AccountStateDelta accountStateDelta;
    public AddressRole addressRole;
    public AlgoSdk.Application application;
    public Asset asset;
}
