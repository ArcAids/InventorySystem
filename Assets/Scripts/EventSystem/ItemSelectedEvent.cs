﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "item selected Event")]
public class ItemSelectedEvent : ScriptableObject
{
    private List<OnItemSelectedEventListener> eventListeners = new List<OnItemSelectedEventListener>();

    public void Select(Item item)
    {
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