using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public class SceneManager
    {
        private const string LogTag = "SCENE";

        public async UniTask LoadScene(int toLoadIndex)
        {
            int currentSceneIndex = UnitySceneManager.GetActiveScene().buildIndex;
            bool isSkipEmpty =
                currentSceneIndex == RuntimeConstants.Scene.Loading ||
                currentSceneIndex == RuntimeConstants.Scene.Bootstrap ||
                toLoadIndex == currentSceneIndex;

            if (isSkipEmpty)
            {
                Log.Default.D(LogTag,$"Empty scene skipped. {SceneUtility.GetScenePathByBuildIndex(toLoadIndex)} is loading.");
                UnitySceneManager.LoadScene(toLoadIndex);
                return;
            }

            bool needLoadEmpty =
                toLoadIndex == RuntimeConstants.Scene.Meta ||
                toLoadIndex == RuntimeConstants.Scene.Match ||
                toLoadIndex == RuntimeConstants.Scene.Loading;

            if (needLoadEmpty)
            {
                Log.Default.D(LogTag,$"{SceneUtility.GetScenePathByBuildIndex(RuntimeConstants.Scene.Empty)} is loading.");
                UnitySceneManager.LoadScene(RuntimeConstants.Scene.Empty);
            }

            await UniTask.NextFrame();
            
            Log.Default.D(LogTag,$"{SceneUtility.GetScenePathByBuildIndex(toLoadIndex)} is loading.");
            UnitySceneManager.LoadScene(toLoadIndex);
        }
    }
}