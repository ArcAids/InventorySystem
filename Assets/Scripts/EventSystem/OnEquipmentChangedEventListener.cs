using UnityEngine;
using UnityEngine.Events;
namespace InventorySystem
{

    public class OnEquipmentChangedEventListener : MonoBehaviour
    {
        public InventoryData gameEvent;
        public UnityEvent responseUpdated;
        public ResponseWithItemData responseEquip;
        public ResponseWithItemData responseDequip;

        private void OnEnable()
        {
            gameEvent.Register(this);

        }
        private void OnDisable()
        {
            gameEvent.DeRegister(this);
        }

        [ContextMenu("Raise Events")]
        public void OnEventRaised()
        {

            if (responseUpdated.GetPersistentEventCount() >= 1)
            {
                responseUpdated.Invoke();
            }
        }

        [ContextMenu("Raise Events")]
        public void OnItemEquipRaised(ItemAndSlot itemUpdated)
        {
            if (responseEquip.GetPersistentEventCount() >= 1)
            {
                responseEquip.Invoke(itemUpdated);
            }
        }

        [ContextMenu("Raise Events")]
        public void OnItemDequipRaised(ItemAndSlot itemUpdated)
        {

            if (responseDequip.GetPersistentEventCount() >= 1)
            {
                responseDequip.Invoke(itemUpdated);
            }
        }
    }

    [System.Serializable]
    public class ResponseWithItemData : UnityEvent<ItemAndSlot>
    {
    }
    [System.Serializable]
    public class ResponseWithItemUI : UnityEvent<ItemUI>
    {
    }

}
