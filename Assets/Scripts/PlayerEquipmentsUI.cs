using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace InventorySystem
{
    public class PlayerEquipmentsUI : MonoBehaviour
    {
        [SerializeField]
        InventoryData equippedItemsData;
        [SerializeField]
        List<ItemHolder> EquipSlots;

        public void EquipItem(Item item, bool isWeaponSlot1)
        {
            int slotIndex = (int)item.slot;
            if (slotIndex==0 && isWeaponSlot1)
                slotIndex = 4;
            ItemUI itemEquipped = EquipSlots[slotIndex].itemUI;
            if (item != itemEquipped.itemInfo)
            {
                if (itemEquipped.itemInfo == null)
                    EquipSlots[slotIndex].AddItem(item);
                else
                    EquipSlots[slotIndex].AddItem(item);
            }
            else
            {
                EquipSlots[slotIndex].RemoveItem();
            }
            equippedItemsData.EquipmentsUpdated();
        }

    }
}
