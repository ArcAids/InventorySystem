﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace InventorySystem
{ 
    public class ItemHolder : MonoBehaviour, IRecievable
    {
        [SerializeField]
        ItemSlot holds;
        [SerializeField]
        PlayerEquipmentsUI player;
        [SerializeField]
        public ItemUI itemUI;


        public IDraggable OnObjectAdded(GameObject selectedObject)
        {
            ItemUI selectedItem=selectedObject.GetComponent<ItemUI>();
            if (selectedItem != null)
            {
                if (selectedItem.itemInfo.slot == holds)
                {
                    AddItem(selectedItem.itemInfo);
                    return selectedItem;
                }
            }
            return null;
        }

        public void AddItem(Item item)
        {
            itemUI.UpdateInfo(item);
        }
        public void RemoveItem()
        {
            itemUI.OnLetGo();
        }

        public void OnObjectHoveringOver(GameObject selectedObject)
        {

        }
    }
}