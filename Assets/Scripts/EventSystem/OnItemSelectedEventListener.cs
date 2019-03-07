using UnityEngine;
using UnityEngine.Events;

public class OnItemSelectedEventListener : MonoBehaviour
{
    public ItemSelectedEvent gameEvent;
    public ResponseWithItem response;

    private void OnEnable()
    {
        gameEvent.Register(this);

    }
    private void OnDisable()
    {
        gameEvent.DeRegister(this);
    }

    [ContextMenu("Raise Events")]
    public void OnEventRaised(Item itemSelected)
    {

        if (response.GetPersistentEventCount() >= 1)
        {
            response.Invoke(itemSelected);
        }
    }
}

[System.Serializable]
public class ResponseWithItem : UnityEvent<Item>
{
}

