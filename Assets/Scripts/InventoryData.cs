using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu]
    public class InventoryData: ScriptableObject
    {
        public EquippedGears gears;
        public bool isDefaultHandRight=true;
        public SortBy sortSave;

        private List<OnEquipmentChangedEventListener> eventListeners = new List<OnEquipmentChangedEventListener>();

        public Item GetItemFromEquipmentsAt(int index)
        {
            switch ((ItemSlot)index)
            {
                case ItemSlot.Weapon:
                    return gears.weapon2Gear;
                case ItemSlot.Head:
                    return gears.headGear;
                case ItemSlot.Body:
                    return gears.bodyGear;
                case ItemSlot.Feet:
                    return gears.LegsGear;
                default:
                    return gears.weapon1Gear;
            }
        }

        public int IsWeaponAlreadyEquipped(string weaponName)
        {
            bool weapon1SlotEmpty = (gears.weapon1Gear == null || gears.weapon1Gear.item_name ==  null || gears.weapon1Gear.item_name == "");
            bool weapon2SlotEmpty = (gears.weapon2Gear == null || gears.weapon2Gear.item_name ==  null || gears.weapon2Gear.item_name ==  "");
            bool isWeaponEquippedAt1 = (!weapon1SlotEmpty && gears.weapon1Gear.item_name == weaponName);
            bool isWeaponEquippedAt2 = (!weapon2SlotEmpty && gears.weapon2Gear.item_name == weaponName);
            if (isWeaponEquippedAt1 || isWeaponEquippedAt2)
            {
                if (isWeaponEquippedAt1)
                    return 4;
                else
                    return 0;
            }
            else if (weapon2SlotEmpty || weapon1SlotEmpty)
            {
                if (weapon1SlotEmpty)
                    return 4;
                else
                    return 0;
            }
            else
            {
                if (isDefaultHandRight)
                    return 4;
                else
                    return 0;
            }
        }

        public bool isItemEquipped(Item item)
        {
            switch (item.slot)
            {
                case ItemSlot.Weapon:
                    if ((gears.weapon1Gear != null && item.item_name == gears.weapon1Gear.item_name)
                        ||
                        (gears.weapon2Gear != null && item.item_name == gears.weapon2Gear.item_name))
                        return true;
                    else
                        return false;
                case ItemSlot.Head:
                    if (gears.headGear != null && item.item_name == gears.headGear.item_name)
                        return true;
                    else
                        return false;
                case ItemSlot.Body:
                    if (gears.bodyGear != null && item.item_name == gears.bodyGear.item_name)
                        return true;
                    else
                        return false;
                case ItemSlot.Feet:
                    if (gears.LegsGear != null && item.item_name == gears.LegsGear.item_name)
                        return true;
                    else
                        return false;
                default:
                    return false;
            }
        }

        public void EquipmentsUpdated()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised();
            }
        }
        public void EquipmentEquiped(ItemAndSlot item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnItemEquipRaised(item);
            }
        }
        public void EquipmentDequiped(ItemAndSlot item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnItemDequipRaised(item);
            }
        }

        public void Register(OnEquipmentChangedEventListener passedEvent)
        {

            if (!eventListeners.Contains(passedEvent))
            {
                eventListeners.Add(passedEvent);
            }
        }

        public void DeRegister(OnEquipmentChangedEventListener passedEvent)
        {
            if (eventListeners.Contains(passedEvent))
            {
                eventListeners.Remove(passedEvent);
            }

        }
    }

    [System.Serializable]
    public class EquippedGears
    {
        public Item headGear;
        public Item weapon1Gear;
        public Item weapon2Gear;
        public Item bodyGear;
        public Item LegsGear;

        public EquippedGears GetClone()
        {
            EquippedGears clone = new EquippedGears
            {
                headGear = headGear,
                weapon1Gear = weapon1Gear,
                weapon2Gear = weapon2Gear,
                bodyGear = bodyGear,
                LegsGear = LegsGear
            };
            return clone;
        }
    }
}