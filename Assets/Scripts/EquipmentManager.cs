using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class EquipmentManager : MonoBehaviour
    {

        [SerializeField]
        InventoryData equipmentsData;

        public void EquipOrDequipItem(Item item)
        {
            if (item == null || item.item_name == "")
                return;
            int slotIndex = (int)item.slot;
            if (item.slot == ItemSlot.Weapon)
            {
                slotIndex = equipmentsDataIsWeaponAlreadyEquipped(item.item_name);
            }
            Item itemEquipped = equipmentsDataGetItemFromEquipmentsAt(slotIndex);
            if (itemEquipped == null)
                {
                    EquipItem(item,slotIndex);          //equip at Empty Place
                }
            else
            if (item.item_name != itemEquipped.item_name)      //is item already in the same slot?
            {
                if (itemEquipped!=null)
                {
                    Debug.Log("Dequipping:" + item.item_name);
                    UnEquipItem(item,slotIndex);                    //remove Item before Adding new one
                }
                                                                        //Equip after Dequiping
                EquipItem(item,slotIndex);
            }
            else
            {
                UnEquipItem(item,slotIndex);                 //Dequip
            }

            equipmentsData.EquipmentsUpdated();
        }
        public void EquipOrDequipItem(Item item, bool weaponHand)
        {
            if (item == null || item.item_name == "")
                return;
            int slotIndex = (int)item.slot;
            if (item.slot == ItemSlot.Weapon)
            {
                slotIndex = equipmentsData.IsWeaponAlreadyEquipped(item.item_name);
            }
            Item itemEquipped = equipmentsData.GetItemFromEquipmentsAt(slotIndex);
            if (itemEquipped == null)
                {
                    EquipItem(item,slotIndex);          //equip at Empty Place
                }
            else
            if (item.item_name != itemEquipped.item_name)      //is item already in the same slot?
            {
                if (itemEquipped!=null)
                {
                    Debug.Log("Dequipping:" + item.item_name);
                    UnEquipItem(item,slotIndex);                    //remove Item before Adding new one
                }
                                                                        //Equip after Dequiping
                EquipItem(item,slotIndex);
            }
            else
            {
                UnEquipItem(item,slotIndex);                 //Dequip
            }

            equipmentsData.EquipmentsUpdated();
        }


        void EquipItem(Item item, int slotIndex)
        {
            Debug.Log("Equipping:" + item.item_name);
            switch (item.slot)
            {
                case ItemSlot.Weapon:
                    {
                        if (slotIndex == 4)
                            equipmentsData.gears.weapon1Gear = item;
                        else
                            equipmentsData.gears.weapon2Gear = item;
                    }
                    break;
                case ItemSlot.Head:
                    equipmentsData.gears.headGear = item;
                    break;
                case ItemSlot.Body:
                    equipmentsData.gears.bodyGear = item;
                    break;
                case ItemSlot.Feet:
                    equipmentsData.gears.LegsGear = item;
                    break;
                default:
                    break;
            }
        }
        void UnEquipItem(Item item,int slotIndex)
        {
            switch (item.slot)
            {
                case ItemSlot.Weapon:
                    {
                        if (slotIndex == 0)
                            equipmentsData.gears.weapon2Gear = null;
                        else
                            equipmentsData.gears.weapon1Gear = null;
                    }
                    break;
                case ItemSlot.Head:
                    equipmentsData.gears.headGear = null;
                    break;
                case ItemSlot.Body:
                    equipmentsData.gears.bodyGear = null;
                    break;
                case ItemSlot.Feet:
                    equipmentsData.gears.LegsGear = null;
                    break;
                default:
                    break;
            }
            equipmentsData.EquipmentDequiped(item);
        }

       

    }
}