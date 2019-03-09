using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace InventorySystem
{ 
    public class ItemHolder : EquipmentManager, IRecievable
    {
        [SerializeField]
        ItemSlot holds;
        [SerializeField]
        bool isWeapon1Slot;
        [SerializeField]
        PlayerEquipmentsUI player;
        [SerializeField]
        public ItemUI itemUI;


        public void OnObjectAdded(GameObject selectedObject)
        {
            ItemUI selectedItem=selectedObject.GetComponent<ItemUI>();
            if (selectedItem != null)
            {
                if (selectedItem.itemInfo.slot == holds)
                {
                    EquipOrDequipItem(selectedItem.itemInfo,CalculateSlotIndex(holds,isWeapon1Slot));
                }
            }
        }

        int CalculateSlotIndex(ItemSlot slot, bool weapon1Slot)
        {
            if (slot == ItemSlot.Weapon)
                return weapon1Slot ? 4 : 0;
            else
                return (int)slot;
        }

        public void AddItem(Item item)
        {
            itemUI.UpdateInfo(item);
            itemUI.Select();
        }
        public void RemoveItem()
        {
            itemUI.itemInfo =null;
            itemUI.RemoveItem();
        }

        public void OnObjectHoveringOver(GameObject selectedObject)
        {

        }
    }
}
