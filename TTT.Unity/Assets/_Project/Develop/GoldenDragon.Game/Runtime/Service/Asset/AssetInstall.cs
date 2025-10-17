using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset
{
    public class AssetInstall
    {
        public  T InstallToRoot<T>(T installObject) where T:UnityEngine.Object
        {
            var instance = UnityEngine.Object.Instantiate(installObject);

            if (instance is GameObject go) go.transform.SetParent(null);

            return instance;
        }

        public T InstallToUiPopup<T>(GameObject installObject,GameObject parent) where T: class
        {
            GameObject instance = UnityEngine.Object.Instantiate(installObject, parent.transform, false);
            instance.SetActive(false);
            return instance.GetComponent<T>();
        }
    }
}