using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class EquipmentManager : MonoBehaviour
    {

        [SerializeField]
        protected InventoryData equipmentsData;
        ItemAndSlot equipmentAndLocation;


        protected void EquipOrDequipItem(Item item)
        {
            if (item == null || item.item_name == null)
                return;
            int slotIndex = (int)item.slot;
            if (item.slot == ItemSlot.Weapon)
            {
                slotIndex = equipmentsData.IsWeaponAlreadyEquipped(item.item_name);
            }
            EquipOrDequipItem(item, slotIndex);
        }

        protected void EquipOrDequipItem(Item item, int slotIndex)
        {
            if (item == null || item.item_name == null)
                return;
            Item itemEquipped = equipmentsData.GetItemFromEquipmentsAt(slotIndex);
            if (itemEquipped == null || itemEquipped.item_name == "")
            {
                EquipItem(item, slotIndex);          //equip at Empty Place
            }
            else
            if (item.item_name != itemEquipped.item_name)      //is item already in the same slot?
            {
                if (itemEquipped != null)
                {
                    UnEquipItem(itemEquipped, slotIndex);                    //remove Item before Adding new one
                }
                                                                            //Equip after Dequiping
                EquipItem(item, slotIndex);
            }
            else
            {
                UnEquipItem(itemEquipped, slotIndex);                 //Dequip
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
            equipmentsData.EquipmentEquiped(new ItemAndSlot(item,slotIndex));
        }
        void UnEquipItem(Item item, int slotIndex)
        {
            Debug.Log("Unequipping:" + item.item_name);
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
            equipmentsData.EquipmentDequiped(new ItemAndSlot(item, slotIndex));
        }



    }

    public class ItemAndSlot
    {
        public Item item;
        public int index;

        public ItemAndSlot(Item item, int index)
        {
            this.item = item;
            this.index = index;
        }
    }
}