using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public class AssetInstall
    {
        public async UniTask InstallToRoot<T>(T installObject) where T:UnityEngine.Object
        {
            var instance = UnityEngine.Object.Instantiate(installObject);

            if (instance is GameObject go) go.transform.SetParent(null);

            await UniTask.Yield();
        }
    }
}