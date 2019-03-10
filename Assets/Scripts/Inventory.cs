using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace InventorySystem
{
    public class Inventory : EquipmentManager,IRecievable
    {
        public enum SortBy
        {
            NAME,
            CLASS,
            TYPE
        }
        [SerializeField]
        ItemConfig itemsList;
        [SerializeField]
        ScrollRect scrollArea;
        [SerializeField]
        ItemUI itemPrefab;
        [SerializeField]
        Transform ItemInventoryParent;

        [SerializeField]
        SortBy sortBy;

        ItemUI selectedItem=null;

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
            Sort();
            
        }

        public void SortByChange(int sortBy)
        {
            this.sortBy = (SortBy)sortBy;
            Sort();
        }

        public void EquipItem(ItemAndSlot item)
        {
            if (inventoryItems.ContainsKey(item.item.item_name))
            {
                Debug.Log("item equiping:"+item.item.item_name);
                inventoryItems[item.item.item_name].RemoveItem();
            }
        }

        public void DequipItem(ItemAndSlot item)
        {
            if (item.item == null)
            {
                Debug.Log("item null", gameObject);
                return;
            }
                
            if (inventoryItems.ContainsKey(item.item.item_name))
            {
                Debug.Log("item dequiping:"+item.item.item_name);
                inventoryItems[item.item.item_name].UpdateInfo(item.item);
                inventoryItems[item.item.item_name].Select();
            }
            Sort();
        }

        void Sort()
        {
            //Array.Sort(inventoryItems,
            //delegate (ItemUI x, ItemUI y) { return x.CompareTo(y); });
            List<ItemUI> itemsList = inventoryItems.Values.ToList<ItemUI>();
            switch (sortBy)
            {
                case SortBy.NAME:
                    itemsList= itemsList.OrderBy(x => x.itemInfo.item_name).ToList();
                    break;
                case SortBy.CLASS:
                    itemsList= itemsList.OrderBy(x => x.itemInfo.item_class).ToList();
                    break;
                case SortBy.TYPE:
                    itemsList= itemsList.OrderBy(x => x.itemInfo.slot).ToList();
                    break;
                default:
                    break;
            }
            int i = 0;
            foreach (var item in itemsList)
            {
                if (!equipmentsData.isItemEquipped(item.itemInfo))
                {
                    item.transform.SetSiblingIndex(i);
                    i++;
                }
                else
                    item.RemoveItem();
            }
        }

       

        public static Color GetClassColor(ItemClass itemClass)
        {
            switch (itemClass)
            {
                case ItemClass.Common:
                    return Color.gray;
                case ItemClass.Uncommon:
                    return Color.green;
                case ItemClass.Rare:
                    return Color.blue;
                case ItemClass.Legendary:
                    return Color.yellow;
                case ItemClass.Mythical:
                    return Color.cyan;
                default:
                    return Color.white;
            }
        }

        public void OnObjectAdded(GameObject selectedObject)
        {
            ItemUI selectedItem = selectedObject.GetComponent<ItemUI>();
            if (selectedItem != null)
            {
                EquipOrDequipItem(selectedItem.itemInfo);
            }
        }

        public void OnObjectHoveringOver(GameObject selectedObject)
        {
            throw new NotImplementedException();
        }

        //public void RefreshList(EquippedGears gearsEquipped)
        //{
        //    foreach (var itemPair in inventoryItems)
        //    {
        //        ItemUI item = itemPair.Value;
        //        switch (item.itemInfo.slot)
        //        {
        //            case ItemSlot.Weapon:
        //                if (gearsEquipped.weapon1Gear != null)
        //                {
        //                    if (item.itemInfo.item_name == gearsEquipped.weapon1Gear.item_name)
        //                    {
        //                        item.HideItem();
        //                        continue;
        //                    }
        //                    else
        //                        item.ShowItem();
        //                }
        //                else
        //                    item.ShowItem();

        //                if (gearsEquipped.weapon2Gear != null)
        //                {
        //                    if (item.itemInfo.item_name == gearsEquipped.weapon2Gear.item_name)
        //                    {
        //                        item.HideItem();
        //                        continue;
        //                    }
        //                    else
        //                        item.ShowItem();
        //                }
        //                else
        //                    item.ShowItem();

        //                break;
        //            case ItemSlot.Head:
        //                if (gearsEquipped.headGear == null)
        //                { item.ShowItem(); continue; }
        //                if (item.itemInfo.item_name == gearsEquipped.headGear.item_name)
        //                {
        //                    item.HideItem();
        //                }
        //                else
        //                    item.ShowItem();
        //                break;
        //            case ItemSlot.Body:
        //                if (gearsEquipped.bodyGear == null)
        //                { item.ShowItem(); continue; }
        //                if (item.itemInfo.item_name == gearsEquipped.bodyGear.item_name)
        //                {
        //                    item.HideItem();
        //                }
        //                else
        //                    item.ShowItem();
        //                break;
        //            case ItemSlot.Feet:
        //                if (gearsEquipped.LegsGear== null)
        //                { item.ShowItem(); continue; }
        //                if (item.itemInfo.item_name == gearsEquipped.LegsGear.item_name)
        //                {
        //                    item.HideItem();
        //                }
        //                else
        //                    item.ShowItem();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}


    }
}
