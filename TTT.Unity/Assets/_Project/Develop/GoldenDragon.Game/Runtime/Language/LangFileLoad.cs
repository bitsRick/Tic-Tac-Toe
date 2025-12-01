using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language
{
    public class LangFileLoad
    {
        public async UniTask<string> Load(string fileName)
        {
            string langFolderPath = Path.Combine(Application.streamingAssetsPath, RuntimeConstants.Lang.FolderName);
            string filePath = Path.Combine(langFolderPath, fileName);

            // Выводим имя файла
            Debug.Log("Имя файла: " + fileName);

            // Читаем содержимое
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            
            await www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string content = www.downloadHandler.text;
                Debug.Log("Содержимое файла " + fileName + ": " + content);
                // Здесь можно обработать JSON, например, с помощью JsonUtility или Newtonsoft.Json
                return content;
            }
            else
            {
                Debug.LogError("Ошибка загрузки файла " + fileName + ": " + www.error);
                return null;
            }
        }
    }
}