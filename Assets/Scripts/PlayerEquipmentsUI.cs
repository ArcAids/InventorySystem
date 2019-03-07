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
        List<ItemHolder> EquipSlots;
        [SerializeField]
        PlayerStats stats;

        private void Start()
        {
            EquipmentsUpdated();
        }
        public void EquipItem(Item item)
        {
            int slotIndex = (int)item.slot;
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
            EquipmentsUpdated();
        }

        public void EquipmentsUpdated()
        {
            stats.UpdateValues(CalculateNewValues());

        }

        public void ItemSelected(Item item)
        {
            stats.PreviewInfo(CalculateNewValues(item));
        }

        ItemData CalculateNewValues(Item item=null) 
        {
            bool oneWeaponChecked = false;
            ItemData overAllStats = new ItemData();
            foreach (var equipment in EquipSlots)
            {
                Item equippedItem = equipment.itemUI.itemInfo;
                if (equippedItem!=null)
                {
                    if (item != null && !oneWeaponChecked && equippedItem.slot == item.slot)
                    {
                        equippedItem = item;
                        if(item.slot==ItemSlot.Weapon)
                        oneWeaponChecked = true;
                    }
                    overAllStats.damage += equipment.itemUI.itemInfo.damage;
                    overAllStats.defence += equipment.itemUI.itemInfo.defence;
                    overAllStats.agility += equipment.itemUI.itemInfo.agility;
                    overAllStats.strength += equipment.itemUI.itemInfo.strength;
                    overAllStats.intel += equipment.itemUI.itemInfo.intel; 
                }
                else
                {

                }
                // TODO: Add additional multiplier values

            }
            return overAllStats;
        }

    }
}
