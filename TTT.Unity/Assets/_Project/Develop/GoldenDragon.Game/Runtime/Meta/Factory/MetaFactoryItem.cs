using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory
{
    public class MetaFactoryItem
    {
        public TItem CreateItem<TItem,TItemContainer>(TItem itemPool,TItemContainer container) 
            where TItem : IItem
            where TItemContainer:IItemContainer 
        {
            TItem item = itemPool;
            item.Image.preserveAspect = true;
            item.Transform.parent = container.Root.gameObject.transform;
                
            RectTransform rt = item.RectTransform;
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.zero;
            rt.localRotation = Quaternion.identity;
            rt.anchoredPosition = Vector2.zero;

            return item;
        }
    }
}