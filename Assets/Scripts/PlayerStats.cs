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
        EquippedGears gears;

        private void Start()
        {
            gears = new EquippedGears();
            overAllStats = new ItemData();
        }

        ItemData CalculateNewValues(Item item = null)
        {
            ItemData overAllStats = new ItemData();
            EquippedGears tempGears = gears;
            tempOverAllStats = new ItemData();
            if (item==null)
            {
                AddStats(gears.headGear);
                AddStats(gears.bodyGear);
                AddStats(gears.LegsGear);
                AddStats(gears.weapon1Gear);
                AddStats(gears.weapon2Gear);
            }
            else
            {
                if (item.slot != ItemSlot.Head)
                    gears.headGear = item;
                else if(item.slot!=ItemSlot.Weapon)
                    gears.weapon1Gear = item;
                else if (item.slot!=ItemSlot.Body)
                    gears.bodyGear = item;
                else if (item.slot!=ItemSlot.Feet)
                    gears.LegsGear = item;

                AddStats(gears.headGear);
                AddStats(gears.bodyGear);
                AddStats(gears.LegsGear);
                AddStats(gears.weapon1Gear);
                AddStats(gears.weapon2Gear);
            }

            return tempOverAllStats;
        }

        bool oneWeaponChecked = false;
        void AddStats(Item equippedItem)
        {
            if (equippedItem != null)
            {
                tempOverAllStats.damage += equippedItem.damage;
                tempOverAllStats.defence += equippedItem.defence;
                tempOverAllStats.agility += equippedItem.agility;
                tempOverAllStats.strength += equippedItem.strength;
                tempOverAllStats.intel += equippedItem.intel;
            }
        }

        public void EquipmentsUpdated(EquippedGears gears)
        {
            this.gears = gears;
            UpdateValues(CalculateNewValues());
        }

        public void OnSelectedItem(Item item)
        {
            PreviewInfo(CalculateNewValues(item));
        }


        public void PreviewInfo(ItemData item)
        {
            //itemBackground.color =;
            damageText.text = GetPreviewString(overAllStats.damage, item.damage);
            defenceText.text = GetPreviewString(overAllStats.defence, item.defence);
            AGIText.text = GetPreviewString(overAllStats.agility, item.agility);
            INTText.text = GetPreviewString(overAllStats.intel, item.intel);
            STRText.text = GetPreviewString(overAllStats.strength, item.strength);
            critText.text = GetPreviewString(overAllStats.critical, item.critical);
            manaText.text = GetPreviewString(overAllStats.mana, item.mana);
            HPText.text = GetPreviewString(overAllStats.HP, item.HP);
            dodgeText.text = GetPreviewString(overAllStats.dodgeChance, item.dodgeChance);
            Debug.Log(item);
        }

        string GetPreviewString(float currentValue, float additionalValue)
        {
            float difference = additionalValue - currentValue;
            string show = currentValue + " ";
            show += difference > 0 ? "+" + difference : difference < 0 ? "" + difference : "";
            return show;
        }

        public void UpdateValues(ItemData item)
        {
            overAllStats = item;

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