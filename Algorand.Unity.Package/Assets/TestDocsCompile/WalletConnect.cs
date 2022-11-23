using Algorand.Unity;
using Algorand.Unity.WalletConnect;
using UnityEngine;

public static class WalletConnectTest
{
    public static async void Main()
    {
        // 1. Create the session and show the user the QR Code
        var dappMeta = new ClientMeta
        {
            Name = "<name of your dapp>",
            Description = "<description of your dapp>",
            Url = "<url of your dapp>",
            IconUrls = new[]
            {
        "<icon1 of your dapp>", "<icon2 of your dapp>"
    }
        };
        var session = new AlgorandWalletConnectSession(dappMeta);
        await session.Connect();
        // session is in no connection status
        Debug.Assert(session.ConnectionStatus == SessionStatus.NoWalletConnected);

        var handshake = session.RequestWalletConnection();
        // session should now be in connecting status
        Debug.Assert(session.ConnectionStatus == SessionStatus.RequestingWalletConnection);

        // show the user a QR Code
        Texture2D qrCode = handshake.ToQrCodeTexture();
        // OR launch a supported wallet app
        WalletRegistry.PeraWallet.LaunchForConnect(handshake);

        // 2. Wait for user to approve the connection
        await session.WaitForWalletApproval();
        // session is now connected
        Debug.Assert(session.ConnectionStatus == SessionStatus.WalletConnected);

        // 3. Send transactions to sign
        var someTxnToSign = Transaction.Payment(default, default, default, default);
        someTxnToSign.Sender = session.Accounts[0];
        var walletTransaction = WalletTransaction.New(someTxnToSign);
        // In the case you used a QR Code, you can await immediately.
        var signingTransactions = session.SignTransactions(new[] { walletTransaction });
        // If you are using Mobile Linking, you need to launch the app
        WalletRegistry.PeraWallet.LaunchForSigning();
        var (signErr, signedTxns) = await signingTransactions;
    }
}
