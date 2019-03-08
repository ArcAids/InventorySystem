using UnityEngine;
using UnityEngine.Events;
namespace InventorySystem
{

    public class OnEquipmentChangedEventListener : MonoBehaviour
    {
        public InventoryData gameEvent;
        public ResponseWithItemData responseForStats;
        public ResponseWithItemUI responseEquip;
        public ResponseWithItemUI responseDequip;

        private void OnEnable()
        {
            gameEvent.Register(this);

        }
        private void OnDisable()
        {
            gameEvent.DeRegister(this);
        }

        [ContextMenu("Raise Events")]
        public void OnEventRaised(EquippedGears itemUpdated)
        {

            if (responseForStats.GetPersistentEventCount() >= 1)
            {
                responseForStats.Invoke(itemUpdated);
            }
        }
        [ContextMenu("Raise Events")]
        public void OnItemEquipRaised(ItemUI itemUpdated)
        {

            if (responseEquip.GetPersistentEventCount() >= 1)
            {
                responseEquip.Invoke(itemUpdated);
            }
        }
        [ContextMenu("Raise Events")]
        public void OnItemDequipRaised(ItemUI itemUpdated)
        {

            if (responseDequip.GetPersistentEventCount() >= 1)
            {
                responseDequip.Invoke(itemUpdated);
            }
        }
    }

    [System.Serializable]
    public class ResponseWithItemData : UnityEvent<EquippedGears>
    {
    }
    [System.Serializable]
    public class ResponseWithItemUI : UnityEvent<ItemUI>
    {
    }

}
