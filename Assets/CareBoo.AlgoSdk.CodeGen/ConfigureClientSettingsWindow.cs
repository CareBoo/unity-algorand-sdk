using System.Linq;
using AlgoSdk;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ConfigureClientSettingsWindow : EditorWindow
{
    [SerializeField]
    AlgodClient algodClient;

    [SerializeField]
    IndexerClient indexerClient;

    [SerializeField]
    KmdClient kmdClient;

    [SerializeField]
    Mnemonic accountMnemonic;

    TextField accountAddressText;

    [MenuItem("Window/AlgoSdk/Configure Client Settings")]
    static void ShowWindow()
    {
        GetWindow<ConfigureClientSettingsWindow>();
    }

    void CreateGUI()
    {
        Load();
        VisualElement root = rootVisualElement;

        root.Add(new Label("Configure Algorand Node Client"));
        var algodClientField = new PropertyField() { bindingPath = nameof(algodClient) };
        var indexerClientField = new PropertyField() { bindingPath = nameof(indexerClient) };
        var kmdClientField = new PropertyField() { bindingPath = nameof(kmdClient) };
        var accountMnemonicField = new PropertyField() { bindingPath = nameof(accountMnemonic) };
        accountAddressText = new TextField("Account Address") { isReadOnly = true };
        var randomizeAccountMnemonicButtonField = new Button(RandomizeAccount) { text = "Randomize Account Mnemonic" };
        var saveButtonField = new Button(Save) { text = "Save" };

        root.Add(algodClientField);
        root.Add(indexerClientField);
        root.Add(kmdClientField);
        root.Add(accountMnemonicField);
        root.Add(accountAddressText);
        root.Add(randomizeAccountMnemonicButtonField);
        root.Add(saveButtonField);
        root.Bind(new SerializedObject(this));
    }

    void Save()
    {
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Algod)), JsonUtility.ToJson(algodClient));
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Indexer)), JsonUtility.ToJson(indexerClient));
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Kmd)), JsonUtility.ToJson(kmdClient));
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.AccountMnemonic)), accountMnemonic.ToString());
    }

    void Load()
    {
        algodClient = AlgoApiClientSettings.Algod;
        indexerClient = AlgoApiClientSettings.Indexer;
        kmdClient = AlgoApiClientSettings.Kmd;
        accountMnemonic = AlgoApiClientSettings.AccountMnemonic;
    }

    void Update()
    {
        accountAddressText.SetValueWithoutNotify(accountMnemonic.ToPrivateKey().ToAddress().ToString());
    }

    void RandomizeAccount()
    {
        accountMnemonic = AlgoSdk.Crypto.Random.Bytes<PrivateKey>().ToMnemonic();
    }
}
