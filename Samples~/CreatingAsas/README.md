# Calling Smart Contract ABI

This sample shows how you can create ASAs (Algorand Smart Assets) in the Unity Editor.

## Usage

1. Select the `AssetCreator` asset.
2. Click the `Generate new Account` button.
3. Click the `Fund Account` button, and fund it using the Algorand dispenser.
4. Once the account is funded, select the `GameToken` asset.
5. Click the `CreateAsset` button, and a Create Asset window should popup.
6. Drag and drop the `AssetCreator` asset to the `Creator Account` field, and the `TestNetAlgod` asset to the `Algod` field.
7. Click the `Create` button.
8. After a couple seconds, you should see an `Asset Created!` message in the console window, and the assets `Index` and `Network` fields should be updated with the new information.
