using UnityEditor;
using UnityEngine;

namespace _Project.Develop.GoldenDragon.Game.Editor.Utility
{
    public class Utility : EditorWindow
    {
        
        [MenuItem("Tools/Save/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}