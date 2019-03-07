using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        ItemConfig itemsList;
        [SerializeField]
        ItemUI prefab;
        [SerializeField]
        Transform ItemInventoryParent;

        private void Start()
        {
            foreach (var item in itemsList.items)
            {
                ItemUI itemUI =Instantiate(prefab, ItemInventoryParent).GetComponent<ItemUI>();
                itemUI.Initialize(item,this);
            }
        }


    }
}
