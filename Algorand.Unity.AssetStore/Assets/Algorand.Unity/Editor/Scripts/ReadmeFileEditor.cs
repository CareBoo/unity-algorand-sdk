using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Algorand.Unity.Readme.Editor
{
    [CustomEditor(typeof(ReadmeFile))]
    public class ReadmeFileEditor : UnityEditor.Editor
    {
        public const string Description = @"Integrate your game with Algorand, a Pure Proof-of-Stake blockchain overseen by the Algorand Foundation. Create Algorand transactions, Algorand accounts, and use Algorand's REST APIs.";
        public Texture2D logo;

        public static string DocHomePath =>
            Path.Combine(Application.dataPath, "Algorand.Unity", "Documentation", "index.html");

        public static PackageInfo PackageInfo =>
            PackageInfo.FindForAssetPath("Packages/com.careboo.unity-algorand-sdk/package.json");

        public override void OnInspectorGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField(new GUIContent(logo),
                    GUILayout.Height(EditorGUIUtility.singleLineHeight * 5f),
                    GUILayout.Width(EditorGUIUtility.singleLineHeight * 5f));
                GUILayout.FlexibleSpace();
            }
            var style = new GUIStyle(EditorStyles.label) { fontSize = 32, alignment = TextAnchor.MiddleCenter };

            GUILayout.Label("Algorand SDK for Unity", style,
                GUILayout.Height(EditorGUIUtility.singleLineHeight * 3));
            var path = DocHomePath;

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("View Documentation"))
                {
                    string url;
                    if (File.Exists(path))
                    {
                        var uriBuilder = new UriBuilder
                        {
                            Scheme = "file",
                            Path = Path.Combine(Application.dataPath, "Algorand.Unity", "Documentation", "index.html")
                        };
                        url = uriBuilder.Uri.ToString();
                    }
                    else
                    {
                        var package = PackageInfo;
                        var splitVersion = package.version.Split('.');
                        var major = splitVersion[0];
                        var minor = splitVersion[1];
                        url = new Uri($"https://careboo.github.io/unity-algorand-sdk/{major}.{minor}/").ToString();
                    }

                    Application.OpenURL(url);
                }
                GUILayout.FlexibleSpace();
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Separator();

            var descriptionStyle = new GUIStyle(EditorStyles.label)
            {
                wordWrap = true,
                richText = true
            };
            GUILayout.Label(Description, descriptionStyle);
        }
    }
}
