using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class PlayerEquipmentsUI : MonoBehaviour
    {
        [SerializeField]
        InventoryData equipmentsData;
        [SerializeField]
        List<ItemHolder> EquipSlots;

        private void OnEnable()
        {
            EquipItem(new ItemAndSlot(equipmentsData.gears.bodyGear, 2));
            EquipItem(new ItemAndSlot(equipmentsData.gears.weapon1Gear, 4));
            EquipItem(new ItemAndSlot(equipmentsData.gears.weapon2Gear, 0));
            EquipItem(new ItemAndSlot(equipmentsData.gears.headGear, 1));
            EquipItem(new ItemAndSlot(equipmentsData.gears.LegsGear, 3));
        }

        public void EquipItem(ItemAndSlot item)
        {
            if (item == null || item.item==null || item.item.item_name == "")
                return;

            EquipSlots[item.index].AddItem(item.item);
        }

        public void DequipItem(ItemAndSlot item)
        {
            if (item == null || item.item == null || item.item.item_name == "")
                return;

            EquipSlots[item.index].RemoveItem();
        }


        //int IsWeaponAlreadyEquipped(string weaponName)
        //{
        //    if (equipmentsData.gears.weapon1Gear==null || equipmentsData.gears.weapon1Gear.item_name == weaponName)
        //        return 4;
        //    if (equipmentsData.gears.weapon2Gear==null  || equipmentsData.gears.weapon2Gear.item_name == weaponName)
        //        return 0;
        //    return -1;
        //}
        //public void UnEquipItemOfName(string itemName)
        //{
        //    ItemHolder itemToUnEquip = null;
        //    foreach (var slot in EquipSlots)
        //    {
        //        if (slot.itemUI.itemInfo.item_name == itemName)
        //        {
        //            itemToUnEquip = slot;
        //            break;
        //        }
        //    }

        //    switch (itemToUnEquip.itemUI.itemInfo.slot)
        //    {
        //        case ItemSlot.Weapon:
        //            {
        //                if (itemName == equipmentsData.gears.weapon2Gear.item_name)
        //                    equipmentsData.gears.weapon2Gear = null;
        //                else
        //                    equipmentsData.gears.weapon1Gear = null;
        //            }
        //            break;
        //        case ItemSlot.Head:
        //            equipmentsData.gears.headGear = null;
        //            break;
        //        case ItemSlot.Body:
        //            equipmentsData.gears.bodyGear = null;
        //            break;
        //        case ItemSlot.Feet:
        //            equipmentsData.gears.LegsGear = null;
        //            break;
        //        default:
        //            break;
        //    }
        //    itemToUnEquip.RemoveItem();
        //}

    }
}
