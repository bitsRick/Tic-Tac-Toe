using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
using UniRx;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory
{
    public class MetaFactoryItem
    {
        public void CreateItemShopSell(Model model,ShopPopup shopPopup, ItemSell itemPull)
        {
            ItemSell item = itemPull;
            item.ImageStyle.preserveAspect = true;
                
            item.ButtonBuy.OnClickAsObservable().Subscribe(_ =>
            {
                model.BuyStyle(item.Id,item.BuyView,item.Type);
            }).AddTo(item);
                
            item.transform.parent = shopPopup.RootInstance.gameObject.transform;
                
            RectTransform rt = item.RectTransform;
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.zero;
            rt.localRotation = Quaternion.identity;
            rt.anchoredPosition = Vector2.zero;
            
            shopPopup.HorizontalLayoutGroup.childScaleHeight = true;
            shopPopup.HorizontalLayoutGroup.childControlHeight = true;
        }

        public void CreateItemInventory(Model model,InventoryPopup popup,ItemInventoryStyle itemPool)
        {
            ItemInventoryStyle item = itemPool;
            
            item.ImageStyle.preserveAspect = true;
                
            item.EnterStyle.OnClickAsObservable().Subscribe(_ =>
            {
                model.EnterStyle(item.Id,item.ActiveStyle,item.Type);
            }).AddTo(item);
                
            item.transform.parent = popup.Root.gameObject.transform;
                
            RectTransform rt = item.RectTransform;
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.zero;
            rt.localRotation = Quaternion.identity;
            rt.anchoredPosition = Vector2.zero;
            
            // popup.HorizontalLayoutGroup.childScaleHeight = true;
            // popup.HorizontalLayoutGroup.childControlHeight = true;
        }
    }
}