using System;
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
            itemBackground.color=Inventory.GetClassColor(itemInfo.item_class);
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
            transform.localScale = Vector3.zero;
            //itemInfo = null;
            isEquipped = false;
        }

        public void RemoveItem()
        {
            HideItem();
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
            itemBackground.color = Inventory.GetClassColor(item.item_class);
            ShowItem();
        }
        public int CompareTo(ItemUI other)
        {
            return 0;
        }

        public int CompareTo(int other)
        {
            throw new NotImplementedException();
        }
    }
}
