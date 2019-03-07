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
        
        Inventory inventory;
        public Item itemInfo;

        public void OnCancelDrag()
        {
            ShowItem();
        } 

        public void OnDeselected()
        {
            itemBackground.color=defaultColor;
        }

        public void OnDragged()
        {

        }

        public void OnLetGo()
        {
            gameObject.SetActive(false);
            itemInfo = null;
        }

        public void OnSelected()
        {
            itemBackground.color=selectedColor;
            itemSelectedEvents.Select(itemInfo);
        }

        public Sprite OnStartDrag()
        {
            HideItem();
            return itemImage.sprite;
        }

        void HideItem()
        {
            transform.localScale = Vector3.zero;
        }
        void ShowItem()
        {
            transform.localScale = Vector3.one;
        }

        public void Initialize(Item item, Inventory inventory)
        {
            this.inventory = inventory;
            UpdateInfo(item);
        }

        public void UpdateInfo(Item item)
        {
            gameObject.SetActive(true);
            itemInfo = item;
            itemImage.sprite = itemInfo.icon;
            itemName.text = itemInfo.item_name;
            ShowItem();
        }
    }
}
