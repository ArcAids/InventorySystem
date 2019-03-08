using System.Collections;
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
        bool isWeapon1Slot;
        [SerializeField]
        PlayerEquipmentsUI player;
        [SerializeField]
        public ItemUI itemUI;


        public GameObject OnObjectAdded(GameObject selectedObject)
        {
            ItemUI selectedItem=selectedObject.GetComponent<ItemUI>();
            if (selectedItem != null)
            {
                if (selectedItem.itemInfo.slot == holds)
                {
                    player.EquipItem(selectedItem.itemInfo,isWeapon1Slot);
                    return itemUI.gameObject;
                }
            }
            return null;
        }

        public void AddItem(Item item)
        {
            itemUI.isEquipped = true;
            itemUI.UpdateInfo(item);
        }
        public void RemoveItem()
        {
            itemUI.isEquipped = false;
            itemUI.itemInfo =null;
            itemUI.OnDragDone();
        }

        public void OnObjectHoveringOver(GameObject selectedObject)
        {

        }
    }
}
