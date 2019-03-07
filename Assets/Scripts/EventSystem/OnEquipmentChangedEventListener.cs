using UnityEngine;
using UnityEngine.Events;
namespace InventorySystem
{

    public class OnEquipmentChangedEventListener : MonoBehaviour
    {
        public InventoryData gameEvent;
        public ResponseWithItemData responseForStats;

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
    }

    [System.Serializable]
    public class ResponseWithItemData : UnityEvent<EquippedGears>
    {
    }

}
