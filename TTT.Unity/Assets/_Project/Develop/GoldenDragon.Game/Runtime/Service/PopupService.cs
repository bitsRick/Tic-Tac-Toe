using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class PopupService:IDisposable
    {
        private Dictionary<string, PopupData> _popupLists = new();
        private readonly SaveLoadService _saveLoadService;
        private readonly ProviderUiFactory _providerUiFactory;
        private readonly AssetService _assetService;
        private PopupBase _activePopup;
        private bool _isActivePopup;
        private bool _isNoClose;

        [Inject]
        public PopupService(SaveLoadService saveLoadService,ProviderUiFactory providerUiFactory,AssetService assetService)
        {
            _assetService = assetService;
            _providerUiFactory = providerUiFactory;
            _saveLoadService = saveLoadService;
        }
        
        public async UniTask AddPopupInList<T>(string pathPopupAsset,GameObject parent) where T: PopupBase
        {
            T popup = default;
            
            if (_popupLists.ContainsKey(typeof(T).Name))
                return;

            try
            {
                GameObject gameObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(pathPopupAsset);
                popup = _assetService.Install.InstallToUiPopup<T>(gameObject, parent);
            }
            catch (Exception e)
            {
                Log.Default.W(nameof(PopupService), $" - error load popup in asset - {e.Message}");
            }
            finally
            {
                _popupLists.Add(typeof(T).Name, new PopupData(popup));
            }
            
            await Task.CompletedTask;
        }
        
        public void Dispose()
        {
            if (_popupLists.Keys.Count < 1)
                return;
            
            Close(true);
            
            List<string> removeKey = new List<string>(_popupLists.Keys);
            
            foreach (var key  in removeKey)
            {
                try
                {
                    _popupLists[key].Dispose();
                }
                catch (Exception e)
                {
                    Log.Default.W(nameof(PopupService),$"Error dispose popup - {key}");
                }
                
                _popupLists.Remove(key);
            }
        }

        public bool TryGetPopup<T>(out T popup) where T : PopupBase
        {
            popup = default;
    
            if (_popupLists.TryGetValue(typeof(T).Name, out PopupData popupData))
            {
                popup = popupData.GetPopup() as T;
                return true;
            }
    
            return false;
        }

        public bool TryOpenPopup<TP>(out TP popupOut)
            where TP : PopupBase
        {
            if (IsNotOpenPopup<TP>())
            {
                if (_activePopup is TP popup)
                {
                    popupOut = popup;
                    return true;
                }
            }

            Log.Default.W($"[PopupService]:NOT OPEN - {typeof(TP).Name}");
            
            popupOut = null;
            return false;
        }

        public void SetNoClose(bool isFlag) => _isNoClose = isFlag;

        public void Close(bool isReset = false)
        {
            if (isReset == false)
            {
                if (_isNoClose)
                    return;
            }
            
            RestActivePopup();
            _saveLoadService.SaveProgress();
        }

        public void RestActivePopup()
        {
            _isActivePopup = false;

            if (_activePopup != null)
            {
                _activePopup.Hide();
                _activePopup = null;
            }
            
            _isNoClose = false;
        }

        private bool IsNotOpenPopup<T>()where T:PopupBase
        {
            if (_isActivePopup == false)
            {
                _isActivePopup = true;
                _activePopup = GetPopup<T>();
                return true;
            }

            return false;
        }

        private PopupBase GetPopup<T>() where T:PopupBase
        {
            return _popupLists.TryGetValue(typeof(T).Name,out PopupData popup) ? popup.GetPopup() : null;
        }
    }
}