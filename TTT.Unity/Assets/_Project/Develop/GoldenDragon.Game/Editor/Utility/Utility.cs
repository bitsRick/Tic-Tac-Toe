using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using UnityEditor;
using UnityEngine;

namespace _Project.Develop.GoldenDragon.Game.Editor.Utility
{
    public class Utility : EditorWindow
    {
        [MenuItem("Tools/Lang/Create Json Rus")]
        public static void CreateLangJson()
        {
            Lang lang = new Lang();
            Debug.Log(JsonUtility.ToJson(lang));
        }
        
        [MenuItem("Tools/Save/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}