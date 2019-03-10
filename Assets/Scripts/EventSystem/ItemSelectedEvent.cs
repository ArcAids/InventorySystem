using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

[CreateAssetMenu(menuName = "item selected Event")]
public class ItemSelectedEvent : ScriptableObject
{
    private List<OnItemSelectedEventListener> eventListeners = new List<OnItemSelectedEventListener>();

    ItemUI selectedItem;

    public void Raise(ItemUI item)
    {
        if (selectedItem != null && item!=selectedItem)
        {
            selectedItem.Deselect();
        }
        selectedItem = item;
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(item);
        }
    }

    public void Register(OnItemSelectedEventListener passedEvent)
    {

        if (!eventListeners.Contains(passedEvent))
        {
            eventListeners.Add(passedEvent);
            if(selectedItem!=null)
                Raise(selectedItem);
        }
    }

    public void DeRegister(OnItemSelectedEventListener passedEvent)
    {
        if (eventListeners.Contains(passedEvent))
        {
            eventListeners.Remove(passedEvent);
        }

    }

}