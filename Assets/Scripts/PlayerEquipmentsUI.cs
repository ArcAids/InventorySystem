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

        private void Start()
        {
            EquipItem(equippedItemsData.gears.bodyGear,true);
            EquipItem(equippedItemsData.gears.weapon1Gear, true);
            EquipItem(equippedItemsData.gears.weapon2Gear, false);
            EquipItem(equippedItemsData.gears.headGear,true);
            EquipItem(equippedItemsData.gears.LegsGear,true);
        }
        public void EquipItem(Item item, bool isWeaponSlot1)
        {
            if (item == null || item.item_name == "")
                return;
            int slotIndex = (int)item.slot;
            if (item.slot==ItemSlot.Weapon)
            {
                int weaponSlot = IsWeaponAlreadyEquipped(item.item_name);
                if (weaponSlot < 0)
                {
                    if (isWeaponSlot1)
                        slotIndex = 4;
                }
                else
                    slotIndex = weaponSlot;
            }
            ItemUI itemEquipped = EquipSlots[slotIndex].itemUI;
            if (item != itemEquipped.itemInfo)      //is item already in the same slot?
            {
                if (itemEquipped.isEquipped)
                {
                    Debug.Log("Dequipping:"+item.item_name);
                    UnEquipItem(slotIndex);
                    //remove Item before Adding new one

                }
                if(!itemEquipped.isEquipped)
                {
                    Debug.Log("Equipping:"+item.item_name);
                    switch (item.slot)
                    {
                        case ItemSlot.Weapon:
                            {
                                if(isWeaponSlot1)
                                    equippedItemsData.gears.weapon1Gear = item;
                                else
                                    equippedItemsData.gears.weapon2Gear = item;
                            }
                            break;
                        case ItemSlot.Head:
                            equippedItemsData.gears.headGear = item;
                            break;
                        case ItemSlot.Body:
                            equippedItemsData.gears.bodyGear = item;
                            break;
                        case ItemSlot.Feet:
                            equippedItemsData.gears.LegsGear = item;
                            break;
                        default:
                            break;
                    }
                    EquipSlots[slotIndex].AddItem(item);
                    equippedItemsData.EquipmentsUpdated(itemEquipped);
                }
            }
            else
            {
                    UnEquipItem(slotIndex);
            }

           // equippedItemsData.EquipmentsUpdated();
        }

        public void UnEquipItem(int slotIndex)
        {
            switch (EquipSlots[slotIndex].itemUI.itemInfo.slot)
            {
                case ItemSlot.Weapon:
                    {
                        if (slotIndex==0)
                            equippedItemsData.gears.weapon2Gear = null;
                        else
                            equippedItemsData.gears.weapon1Gear = null;
                    }
                    break;
                case ItemSlot.Head:
                    equippedItemsData.gears.headGear = null;
                    break;
                case ItemSlot.Body:
                    equippedItemsData.gears.bodyGear = null;
                    break;
                case ItemSlot.Feet:
                    equippedItemsData.gears.LegsGear = null;
                    break;
                default:
                    break;
            }
            equippedItemsData.EquipmentDequiped(EquipSlots[slotIndex].itemUI);
            EquipSlots[slotIndex].RemoveItem();
        }

        int IsWeaponAlreadyEquipped(string weaponName)
        {
            if (equippedItemsData.gears.weapon1Gear!=null && equippedItemsData.gears.weapon1Gear.item_name == weaponName)
                return 4;
            if (equippedItemsData.gears.weapon2Gear!=null  && equippedItemsData.gears.weapon2Gear.item_name == weaponName)
                return 0;
            return -1;
        }
        public void UnEquipItemOfName(string itemName)
        {
            ItemHolder itemToUnEquip = null;
            foreach (var slot in EquipSlots)
            {
                if (slot.itemUI.itemInfo.item_name == itemName)
                {
                    itemToUnEquip = slot;
                    break;
                }
            }

            switch (itemToUnEquip.itemUI.itemInfo.slot)
            {
                case ItemSlot.Weapon:
                    {
                        if (itemName == equippedItemsData.gears.weapon2Gear.item_name)
                            equippedItemsData.gears.weapon2Gear = null;
                        else
                            equippedItemsData.gears.weapon1Gear = null;
                    }
                    break;
                case ItemSlot.Head:
                    equippedItemsData.gears.headGear = null;
                    break;
                case ItemSlot.Body:
                    equippedItemsData.gears.bodyGear = null;
                    break;
                case ItemSlot.Feet:
                    equippedItemsData.gears.LegsGear = null;
                    break;
                default:
                    break;
            }
            equippedItemsData.EquipmentDequiped(itemToUnEquip.itemUI);
            itemToUnEquip.RemoveItem();
        }

    }
}
