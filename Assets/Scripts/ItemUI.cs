using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InventorySystem
{
    public class ItemUI : MonoBehaviour, IDraggable
    {
        [SerializeField]
        ItemSelectedEvent itemSelectedEvents;
        [SerializeField]
        Image itemImage;
        [SerializeField]
        TMP_Text itemName;
        [SerializeField]
        Image itemBackground;
        [SerializeField]
        Color selectedColor;
        [SerializeField]
        Color defaultColor;
        
        ScrollRect scrollView;
        public Item itemInfo;
        public bool isEquipped=false;

        public void OnCancelDrag()
        {
            if(scrollView!=null)
            scrollView.enabled = true;
            ShowItem();
        } 

        public void Deselect()
        {
            //Debug.Log(itemInfo.item_name+" deselecting.");
            itemBackground.color=GetClassColor(itemInfo.item_class);
        }

        public void OnDragDone()
        {
            if(scrollView!=null)
            scrollView.enabled = true;
            //Deselect();
            //HideItem();
        }

        public void Select()
        {
            //Debug.Log(itemInfo.item_name+" Selected.");
            itemBackground.color=selectedColor;
            itemSelectedEvents.Raise(itemInfo);
        }



        public Sprite OnStartDrag()
        {
            HideItem();
            if(scrollView!=null)
            scrollView.enabled = false;
            return itemImage.sprite;
        }

        public void HideItem()
        {
            Debug.Log(itemInfo.item_name+" Hiding.");
            transform.localScale = Vector3.zero;
            //itemInfo = null;
            isEquipped = false;
            gameObject.SetActive(false);
        }
        public void ShowItem()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.one;
        }

        public void Initialize(Item item, ScrollRect scrollView)
        {
            this.scrollView = scrollView;
            UpdateInfo(item);
        }

        public void UpdateInfo(Item item)
        {
            itemInfo = item;
            itemImage.sprite = itemInfo.icon;
            itemName.text = itemInfo.item_name;
            itemBackground.color = GetClassColor(item.item_class);
            ShowItem();
        }

        Color GetClassColor(ItemClass itemClass)
        {
            switch (itemClass)
            {
                case ItemClass.Common:
                    return Color.gray;
                case ItemClass.Uncommon:
                    return Color.green;
                case ItemClass.Rare:
                    return Color.blue;
                case ItemClass.Legendary:
                    return Color.yellow;
                case ItemClass.Mythical:
                    return Color.cyan;
                default:
                    return defaultColor;
            }
        }
    }
}
