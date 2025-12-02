using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class PopupService
    {
        private Dictionary<string, PopupData> _popupLists = new();
        private readonly SaveLoadService _saveLoadService;
        private readonly ProviderUiFactory _providerUiFactory;
        private readonly AssetService _assetService;
        private PopupBase _activePopup;
        private bool _isActivePopup;
        private bool _isNoClose;

        public PopupService(SaveLoadService saveLoadService,ProviderUiFactory providerUiFactory,AssetService assetService)
        {
            _assetService = assetService;
            _providerUiFactory = providerUiFactory;
            _saveLoadService = saveLoadService;
        }
        
        public async void AddPopupInList<T>(string pathPopupAsset,GameObject parent) where T: PopupBase
        {
            T popup = default;
            
            if (_popupLists.ContainsKey(nameof(T)))
                return;
            
            try
            {
                GameObject gameObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(pathPopupAsset);
                popup = _assetService.Install.InstallToUiPopup<T>(gameObject, parent);
            }
            catch (Exception e)
            {
                Log.Default.W(nameof(PopupService),$" - error load popup in asset - {e.Message}");
            }
            
            _popupLists.Add(nameof(T), new PopupData(popup));
        }

        public async UniTask Release()
        {
            foreach (KeyValuePair<string,PopupData> popupData in _popupLists) 
                popupData.Value.Dispose();

            _popupLists = null;

            await UniTask.CompletedTask;
        }

        public bool TryGetPopup<T>(out T popup) where T : class
        {
            popup = default;
    
            if (_popupLists.TryGetValue(nameof(T), out PopupData popupData))
            {
                popup = popupData.GetPopup() as T;
                return true;
            }
    
            return false;
        }

        public bool TryOpenPopup<TP>(out TP popupOut)
            where TP : class
        {
            if (IsNotOpenPopup<TP>())
            {
                if (_activePopup is TP popup)
                {
                    popupOut = popup;
                    return true;
                }
            }

            Log.Default.W($"[PopupService]:NOT OPEN - {nameof(TP)}");
            
            popupOut = null;
            return false;
        }

        public void SetNoClose(bool isFlag) => _isNoClose = isFlag;

        public void Close()
        {
            AudioPlayer.S.Click();
            
            if (_isNoClose)
                return;

            Rest();
            _saveLoadService.SaveProgress();
        }

        private void Rest()
        {
            _isActivePopup = false;
            _activePopup.Hide();
            _activePopup = null;
        }

        private bool IsNotOpenPopup<T>()where T:class
        {
            if (_isActivePopup == false)
            {
                _isActivePopup = true;
                _activePopup = GetPopup<T>();
                return true;
            }

            return false;
        }

        private PopupBase GetPopup<T>() where T:class
        {
            return _popupLists.TryGetValue(nameof(T),out PopupData popup) ? popup.GetPopup() : null;
        }
    }
}