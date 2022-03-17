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

    TextField accountAddressText;

    [MenuItem("AlgoSdk/Configure Client Settings")]
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
        var saveButtonField = new Button(Save) { text = "Save" };

        root.Add(algodClientField);
        root.Add(indexerClientField);
        root.Add(kmdClientField);
        root.Add(saveButtonField);
        root.Bind(new SerializedObject(this));
    }

    void Save()
    {
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Algod)), JsonUtility.ToJson(algodClient));
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Indexer)), JsonUtility.ToJson(indexerClient));
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Kmd)), JsonUtility.ToJson(kmdClient));
    }

    void Load()
    {
        algodClient = AlgoApiClientSettings.Algod;
        indexerClient = AlgoApiClientSettings.Indexer;
        kmdClient = AlgoApiClientSettings.Kmd;
    }
}
