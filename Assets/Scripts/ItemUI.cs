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
        
        ScrollRect scrollView;
        public Item itemInfo;

        public void OnCancelDrag()
        {
            if(scrollView!=null)
            scrollView.enabled = true;
            ShowItem();
        } 

        public void Deselect()
        {

            gameObject.transform.localScale = Vector3.one;
            //itemBackground.color=Inventory.GetClassColor(itemInfo.item_class);
        }

        public void Select()
        {
            gameObject.transform.localScale = Vector3.one * 1.2f;
            itemSelectedEvents.Raise(this);
        }
        public void OnDragDone()
        {
            if(scrollView!=null)
            scrollView.enabled = true;
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
    }
}
