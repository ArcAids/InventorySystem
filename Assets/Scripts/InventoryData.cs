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

        public void EquipmentsUpdated()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(gears);
            }
        }
        public void EquipmentsUpdated(ItemUI item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnItemEquipRaised(item);
            }
        }
        public void EquipmentDequiped(ItemUI item)
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