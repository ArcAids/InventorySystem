using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu]
    public class InventoryData: ScriptableObject
    {
        public EquippedGears gears;

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
            if (gears.weapon1Gear != null && gears.weapon1Gear.item_name == weaponName)
                return 4;
            if (gears.weapon2Gear != null && gears.weapon2Gear.item_name == weaponName)
                return 0;
            if (gears.weapon2Gear == null)
                return 0;
            else
                return 4;
        }

        public void EquipmentsUpdated()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised();
            }
        }
        public void EquipmentEquiped(ItemUI item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnItemEquipRaised(item);
            }
        }
        public void EquipmentDequiped(Item item)
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

    }
}