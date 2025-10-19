using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace _Project.Develop.GoldenDragon.Game.Editor.AddresablleUtilites
{
    public class AddressablesKeysViewer : EditorWindow 
    {
        private Vector2 scrollPosition;
        private List<string> allKeys = new List<string>();

        [MenuItem("Tools/Addressables/View All Keys")]
        public static void ShowWindow() // Этот метод может быть static
        {
            GetWindow<AddressablesKeysViewer>("Addressables Keys");
        }

        private void OnGUI()
        {
            GUILayout.Label("Addressables Keys Viewer", EditorStyles.boldLabel);

            if (GUILayout.Button("Refresh Keys", GUILayout.Height(30)))
            {
                RefreshKeys();
            }

            if (GUILayout.Button("Copy All Keys to Clipboard", GUILayout.Height(25)))
            {
                CopyKeysToClipboard();
            }

            GUILayout.Space(10);
            GUILayout.Label($"Found {allKeys.Count} keys:", EditorStyles.boldLabel);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (var key in allKeys)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(key);

                if (GUILayout.Button("Copy", GUILayout.Width(50)))
                {
                    EditorGUIUtility.systemCopyBuffer = key;
                    Debug.Log($"Copied: {key}");
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }

        private void RefreshKeys()
        {
            allKeys.Clear();

            var settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressable Assets Settings not found!");
                return;
            }

            foreach (var group in settings.groups)
            {
                if (group == null) continue;

                foreach (var entry in group.entries)
                {
                    if (entry == null) continue;

                    string address = entry.address;
                    if (!string.IsNullOrEmpty(address) && !allKeys.Contains(address))
                    {
                        allKeys.Add(address);
                    }

                    foreach (var label in entry.labels)
                    {
                        if (!string.IsNullOrEmpty(label) && !allKeys.Contains(label))
                        {
                            allKeys.Add(label);
                        }
                    }
                }
            }

            allKeys.Sort();
            Debug.Log($"Refreshed! Found {allKeys.Count} unique keys");
        }

        private void CopyKeysToClipboard()
        {
            if (allKeys.Count == 0)
            {
                RefreshKeys();
            }

            string keysText = string.Join("\n", allKeys);
            EditorGUIUtility.systemCopyBuffer = keysText;
            Debug.Log($"Copied {allKeys.Count} keys to clipboard");
        }

        private void OnFocus()
        {
            RefreshKeys();
        }
    }
}