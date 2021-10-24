Setup Sender Account
--------------------

Now that you've connected to the node, the next step is setting up the sender account that will
make the payment transaction. The sandbox node starts with a couple accounts that already have
algo balances. You can list available accounts via the sandbox CLI:
```bash
> ./sandbox goal account list
[online]        FLWI6UNTQ6CXTKSHOC7QPHYD2L3JVLIPWKNR5FECHX46VOE3DMY24BJASY      FLWI6UNTQ6CXTKSHOC7QPHYD2L3JVLIPWKNR5FECHX46VOE3DMY24BJASY      2000000000000000 microAlgos
[online]        VIOMNPG4N4UF2TUJMOWJRX3Z56NS3SMPMWM2NOTEBDLZ32PPMOHQPLDQ2M      VIOMNPG4N4UF2TUJMOWJRX3Z56NS3SMPMWM2NOTEBDLZ32PPMOHQPLDQ2M      4000000000000000 microAlgos
[online]        2HALGWMIGEFVZRWC3O536UNUY4ABVLZCSBKJI5RC7HE3H64FHGWFVYE6E4      2HALGWMIGEFVZRWC3O536UNUY4ABVLZCSBKJI5RC7HE3H64FHGWFVYE6E4      4000000000000000 microAlgos
```
Copy the account address with a good number of `microAlgos`. We're going to use this account
to send the payment transaction. In this case, I'm going to use `VIOMNPG4N4UF2TUJMOWJRX3Z56NS3SMPMWM2NOTEBDLZ32PPMOHQPLDQ2M`.

To be able to send a transaction, we'll need the private key of the account. Export the private key
using the account address you copied earlier.
```bash
> ./sandbox goal account export --address VIOMNPG4N4UF2TUJMOWJRX3Z56NS3SMPMWM2NOTEBDLZ32PPMOHQPLDQ2M
Exported key for account VIOMNPG4N4UF2TUJMOWJRX3Z56NS3SMPMWM2NOTEBDLZ32PPMOHQPLDQ2M: "find paddle girl crumble hammer usual obvious toy actual obscure decorate lock bag inmate author valve course ship burger denial sibling tissue nominee above remove"
```
The key should be printed as a mnemonic of 25 words. In this case it's `"find paddle girl crumble hammer usual obvious toy actual obscure decorate lock bag inmate author valve course ship burger denial sibling tissue nominee above remove"`.

> [!Note]
> The first 24 words of a mnemonic encode the 64 byte private key in the account. The last word is used as a checksum validating the mnemonic.

With the mnemonic, it's straightforward to get the account's private key and address.
```csharp
// using AlgoSdk;

Mnemonic mnemonic = "find paddle girl crumble hammer usual obvious toy actual obscure decorate lock bag inmate author valve course ship burger denial sibling tissue nominee above remove";

PrivateKey privateKey = mnemonic.ToPrivateKey();
Address address = privateKey.ToPublicKey();
```
