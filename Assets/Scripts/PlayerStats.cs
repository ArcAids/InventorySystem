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

        private void Awake()
        {
            overAllStats = new ItemData();
        }

        ItemData CalculateNewValues(Item item = null)
        {
            tempOverAllStats = new ItemData();
            
                AddStats(equipments.gears.headGear, tempOverAllStats);
                AddStats(equipments.gears.bodyGear, tempOverAllStats);
                AddStats(equipments.gears.LegsGear, tempOverAllStats);
                AddStats(equipments.gears.weapon1Gear, tempOverAllStats);
                AddStats(equipments.gears.weapon2Gear, tempOverAllStats);
            if (item!=null)
            {
                Item itemAlreadyEquipped = null;
                switch (item.slot)
                {
                    case ItemSlot.Weapon:
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
                if(itemAlreadyEquipped!=null && item.item_name!=itemAlreadyEquipped.item_name)
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

        //public void EquipmentsUpdated(EquippedGears gears)
        //{
        //    this.gears = gears;
        //    UpdateValues(CalculateNewValues());
        //}

        public void ItemEquipped(ItemUI item)
        {
            AddStats(item.itemInfo,overAllStats);
            CalculateAdditionalStats(overAllStats);
            UpdateUIValues();
        }
        public void ItemDequipped(ItemUI item)
        {
            SubstractStatsForItem(item.itemInfo,overAllStats);
            CalculateAdditionalStats(overAllStats);
            UpdateUIValues();
        }

        public void OnSelectedItem(Item item)
        {
            PreviewInfo(CalculateNewValues(item));
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
            damageText.text = overAllStats.damage + "";
            defenceText.text = overAllStats.defence + "";
            AGIText.text = overAllStats.agility + "";
            INTText.text = overAllStats.intel + "";
            STRText.text = overAllStats.strength + "";
            critText.text = overAllStats.critical + "";
            manaText.text = overAllStats.mana + "";
            HPText.text = overAllStats.HP + "";
            dodgeText.text = overAllStats.dodgeChance + "";
        }
    }
}