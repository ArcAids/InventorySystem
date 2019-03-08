using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        ItemConfig itemsList;
        [SerializeField]
        ScrollRect scrollArea;
        [SerializeField]
        ItemUI itemPrefab;
        [SerializeField]
        Transform ItemInventoryParent;

        ItemUI selectedItem=null;
        List<ItemUI> hiddenItems = new List<ItemUI>();

        Dictionary<string, ItemUI> inventoryItems = new Dictionary<string, ItemUI>();

        private void Awake()
        {
            foreach (var item in itemsList.items)
            {
                ItemUI itemUI =Instantiate(itemPrefab, ItemInventoryParent).GetComponent<ItemUI>();
                itemUI.Initialize(item,scrollArea);
                inventoryItems.Add(item.item_name,itemUI);
            }
        }
        private void Start()
        {
            //if (inventoryItems.Count > 0)
            //    inventoryItems[0].Select();
            
        }

        public void SelectItem(Item item)
        {
            if (inventoryItems.ContainsKey(item.item_name))
            {
                if(selectedItem!=null && selectedItem.itemInfo!=item)
                    selectedItem.Deselect();
                selectedItem = inventoryItems[item.item_name];
            }
        }
            
        public void EquipDequipItem(ItemUI item)
        {
            if (inventoryItems.ContainsKey(item.itemInfo.item_name))
            {
                Debug.Log("item equiping:"+item.itemInfo.item_name);
                inventoryItems[item.itemInfo.item_name].HideItem();
                hiddenItems.Add(inventoryItems[item.itemInfo.item_name]);
                inventoryItems[item.itemInfo.item_name] = item;
                item.Select();
            }
        }

        public void DequipItem(ItemUI item)
        {
            if (inventoryItems.ContainsKey(item.itemInfo.item_name))
            {
                Debug.Log("item dequiping:"+item.itemInfo.item_name);
                inventoryItems[item.itemInfo.item_name].HideItem();
                inventoryItems[item.itemInfo.item_name] = hiddenItems[0];
                hiddenItems.RemoveAt(0);
                inventoryItems[item.itemInfo.item_name].UpdateInfo(item.itemInfo);
                inventoryItems[item.itemInfo.item_name].Select();
            }
        }



        public void RefreshList(EquippedGears gearsEquipped)
        {
            foreach (var itemPair in inventoryItems)
            {
                ItemUI item = itemPair.Value;
                switch (item.itemInfo.slot)
                {
                    case ItemSlot.Weapon:
                        if (gearsEquipped.weapon1Gear != null)
                        {
                            if (item.itemInfo.item_name == gearsEquipped.weapon1Gear.item_name)
                            {
                                item.HideItem();
                                continue;
                            }
                            else
                                item.ShowItem();
                        }
                        else
                            item.ShowItem();

                        if (gearsEquipped.weapon2Gear != null)
                        {
                            if (item.itemInfo.item_name == gearsEquipped.weapon2Gear.item_name)
                            {
                                item.HideItem();
                                continue;
                            }
                            else
                                item.ShowItem();
                        }
                        else
                            item.ShowItem();

                        break;
                    case ItemSlot.Head:
                        if (gearsEquipped.headGear == null)
                        { item.ShowItem(); continue; }
                        if (item.itemInfo.item_name == gearsEquipped.headGear.item_name)
                        {
                            item.HideItem();
                        }
                        else
                            item.ShowItem();
                        break;
                    case ItemSlot.Body:
                        if (gearsEquipped.bodyGear == null)
                        { item.ShowItem(); continue; }
                        if (item.itemInfo.item_name == gearsEquipped.bodyGear.item_name)
                        {
                            item.HideItem();
                        }
                        else
                            item.ShowItem();
                        break;
                    case ItemSlot.Feet:
                        if (gearsEquipped.LegsGear== null)
                        { item.ShowItem(); continue; }
                        if (item.itemInfo.item_name == gearsEquipped.LegsGear.item_name)
                        {
                            item.HideItem();
                        }
                        else
                            item.ShowItem();
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
