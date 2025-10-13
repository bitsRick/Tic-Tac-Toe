using UnityEditor;
using UnityEngine;

namespace _Project.Develop.GoldenDragon.Game.Editor.AddresablleUtilites
{
    public class EditorInputDialog : EditorWindow
    {
        private string inputText = "";
        private System.Action<string> onOk;
    
        public static string Show(string title, string message, string defaultValue = "")
        {
            var window = CreateInstance<EditorInputDialog>();
            window.titleContent = new GUIContent(title);
            window.inputText = defaultValue;
            window.minSize = new Vector2(300, 120);
            window.ShowModal();
            return window.inputText;
        }
    
        private void OnGUI()
        {
            GUILayout.Label("Enter search term:", EditorStyles.boldLabel);
            inputText = EditorGUILayout.TextField(inputText);
        
            GUILayout.FlexibleSpace();
        
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("OK"))
            {
                Close();
            }
            if (GUILayout.Button("Cancel"))
            {
                inputText = "";
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}