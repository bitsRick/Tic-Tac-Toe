using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset
{
    public class AssetInstall
    {
        public  T InstallToRoot<T>(T installObject) where T:UnityEngine.Object
        {
            T instance = null;
            
            try
            {
                instance = UnityEngine.Object.Instantiate(installObject);
            }
            catch (MissingReferenceException)
            {
                Log.Default.W(nameof(AssetInstall),"Prefab не назначен или уничтожен");
            }
            catch (UnityException ex) when (ex.Message.Contains("GameObject"))
            {
                Log.Default.W(nameof(AssetInstall),"Проблема с GameObject");
            }

            if (instance is GameObject go) 
                go.transform.SetParent(null);

            return instance;
        }

        public T InstallToUiPopup<T>(GameObject installObject,GameObject parent) where T: PopupBase
        {
            GameObject instance = null;
            
            try
            {
                instance = UnityEngine.Object.Instantiate(installObject,parent.transform);
            }
            catch (MissingReferenceException)
            {
                Log.Default.W(nameof(AssetInstall),"Prefab не назначен или уничтожен");
            }
            catch (UnityException ex) when (ex.Message.Contains("GameObject"))
            {
                Log.Default.W(nameof(AssetInstall),"Проблема с GameObject");
            }
            
            instance.SetActive(false);
            return instance.GetComponent<T>();
        }

        public T InstallToGameObject<T>(T installObject)where T:UnityEngine.Object
        {
            return Object.Instantiate(installObject);
        }
    }
}