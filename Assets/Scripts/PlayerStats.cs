using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace InventorySystem
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        TMP_Text damageText;
        [SerializeField]
        TMP_Text defenceText;
        [SerializeField]
        TMP_Text STRText;
        [SerializeField]
        TMP_Text AGIText;
        [SerializeField]
        TMP_Text INTText;
        [SerializeField]
        TMP_Text HPText;
        [SerializeField]
        TMP_Text manaText;
        [SerializeField]
        TMP_Text critText;
        [SerializeField]
        TMP_Text dodgeText;

        ItemData overAllStats;
        ItemData tempOverAllStats;
        [SerializeField]
        InventoryData equipments;

        EquippedGears previewEquipments;

        private void OnEnable()
        {
            EquipmentsUpdated();
        }


        ItemData CalculateNewValues(Item item = null)
        {
            tempOverAllStats = new ItemData();
            GetCurrentValues(tempOverAllStats);
            if (item!=null)
            {
                Item itemAlreadyEquipped = null;
                switch (item.slot)
                {
                    case ItemSlot.Weapon:
                        if(equipments.gears.weapon2Gear!=null && equipments.gears.weapon2Gear.item_name == item.item_name)
                            itemAlreadyEquipped = equipments.gears.weapon2Gear;
                        else
                            itemAlreadyEquipped = equipments.gears.weapon1Gear;

                        break;
                    case ItemSlot.Head:
                        itemAlreadyEquipped= equipments.gears.headGear;
                        break;
                    case ItemSlot.Body:
                        itemAlreadyEquipped= equipments.gears.bodyGear;
                        break;
                    case ItemSlot.Feet:
                        itemAlreadyEquipped= equipments.gears.LegsGear;
                        break;
                    default:
                        break;
                }

                SubstractStatsForItem(itemAlreadyEquipped, tempOverAllStats);
                if(itemAlreadyEquipped==null || (itemAlreadyEquipped!=null && item.item_name!=itemAlreadyEquipped.item_name))
                    AddStats(item, tempOverAllStats);
            }
            CalculateAdditionalStats(tempOverAllStats);
            return tempOverAllStats;
        }


        void CalculateAdditionalStats(ItemData data)
        {
            data.critical = data.agility * 0.15f;
            data.dodgeChance = data.agility * 0.2f;
            data.HP = data.strength * 12;
            data.mana= data.intel * 14;
        }

        void GetCurrentValues(ItemData stats)
        {
            AddStats(equipments.gears.headGear, stats);
            AddStats(equipments.gears.bodyGear, stats);
            AddStats(equipments.gears.LegsGear, stats);
            AddStats(equipments.gears.weapon1Gear, stats);
            AddStats(equipments.gears.weapon2Gear, stats);
        }

        void AddStats(Item equippedItem, ItemData stats)
        {
            if (equippedItem != null)
            {
                stats.damage += equippedItem.damage;
                stats.defence += equippedItem.defence;
                stats.agility += equippedItem.agility;
                stats.strength += equippedItem.strength;
                stats.intel += equippedItem.intel;
            }
        }
        void SubstractStatsForItem(Item equippedItem, ItemData stats)
        {
            if (equippedItem != null)
            {
                stats.damage -= equippedItem.damage;
                stats.defence -= equippedItem.defence;
                stats.agility -= equippedItem.agility;
                stats.strength -= equippedItem.strength;
                stats.intel -= equippedItem.intel;
            }
        }

        public void EquipmentsUpdated()
        {
            overAllStats = new ItemData();
            GetCurrentValues(overAllStats);
            CalculateAdditionalStats(overAllStats);
            UpdateUIValues();
        }

        public void OnSelectedItem(ItemUI item)
        {
            PreviewInfo(CalculateNewValues(item.itemInfo));
        }

        public void PreviewInfo(ItemData item)
        {
            SetPreviewString(damageText ,overAllStats.damage, item.damage);
            SetPreviewString(defenceText,overAllStats.defence, item.defence);
            SetPreviewString(AGIText,overAllStats.agility, item.agility);
            SetPreviewString(INTText,overAllStats.intel, item.intel);
            SetPreviewString(STRText,overAllStats.strength, item.strength);
            SetPreviewString(critText,overAllStats.critical, item.critical);
            SetPreviewString(manaText,overAllStats.mana, item.mana);
            SetPreviewString(HPText,overAllStats.HP, item.HP);
            SetPreviewString(dodgeText,overAllStats.dodgeChance, item.dodgeChance);
        }

        void SetPreviewString(TMP_Text statSlot,float currentValue, float additionalValue)
        {
            float difference = additionalValue - currentValue;
            string show = currentValue + " ";
            if (difference < 0)
            {
                statSlot.color = Color.red;
                show += "" + difference;
            }
            else if (difference > 0)
            {
                statSlot.color = Color.green;
                show += "+" + difference;
            }
            else
                statSlot.color = Color.white;

            statSlot.text = show;
        }

        public void UpdateUIValues()
        {
            SetFinalStatString(damageText, overAllStats.damage);
            SetFinalStatString(defenceText, overAllStats.defence);
            SetFinalStatString(AGIText, overAllStats.agility);
            SetFinalStatString(INTText, overAllStats.intel);
            SetFinalStatString(STRText, overAllStats.strength);
            SetFinalStatString(critText, overAllStats.critical);
            SetFinalStatString(manaText, overAllStats.mana);
            SetFinalStatString(HPText, overAllStats.HP);
            SetFinalStatString(dodgeText, overAllStats.dodgeChance);
        }

        void SetFinalStatString(TMP_Text statSlot,float currentValue)
        {
            string show = currentValue + " ";
            statSlot.color = Color.white;
            statSlot.text = show;
        }
        
        public void ItemEquipped(ItemAndSlot item)
        {
            if (item == null)
                return;
            AddStats(item.item, overAllStats);
            CalculateAdditionalStats(overAllStats);
            UpdateUIValues();
        }
        public void ItemDequipped(ItemAndSlot item)
        {
            if (item == null)
                return;
            SubstractStatsForItem(item.item, overAllStats);
            CalculateAdditionalStats(overAllStats);
            UpdateUIValues();
        }
    }
}