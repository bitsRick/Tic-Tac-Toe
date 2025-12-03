using UnityEditor;
using UnityEngine;

namespace _Project.Develop.GoldenDragon.Game.Editor.AddresablleUtilites
{
    public class EditorInputDialog : EditorWindow
    {
        private string inputText = "";
        private System.Action<string> onOk;
        
        private void OnGUI()
        {
            GUILayout.Label("Enter search term:", EditorStyles.boldLabel);
            inputText = EditorGUILayout.TextField(inputText);
        
            GUILayout.FlexibleSpace();
        
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("OK")) 
                Close();

            if (GUILayout.Button("Cancel"))
            {
                inputText = "";
                Close();
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}