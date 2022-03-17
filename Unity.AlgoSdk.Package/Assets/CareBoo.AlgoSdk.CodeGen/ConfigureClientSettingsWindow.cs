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
        var algodJson = JsonUtility.ToJson(algodClient);
        var indexerJson = JsonUtility.ToJson(indexerClient);
        var kmdJson = JsonUtility.ToJson(kmdClient);

        Debug.Log($"Saving clients settings:\nAlgod: {algodJson}\nIndexer: {indexerJson}\nKmd: {kmdJson}");
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Algod)), algodJson);
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Indexer)), indexerJson);
        EditorPrefs.SetString(AlgoApiClientSettings.GetKey(nameof(AlgoApiClientSettings.Kmd)), kmdJson);
    }

    void Load()
    {
        algodClient = AlgoApiClientSettings.Algod;
        indexerClient = AlgoApiClientSettings.Indexer;
        kmdClient = AlgoApiClientSettings.Kmd;
    }
}
