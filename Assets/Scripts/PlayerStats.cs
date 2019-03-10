using UnityEngine;
using TMPro;
using System;

[Serializable]
public class MythicalBoost
{
    public StatToBoost boostsThis;
    public float byPercentage;
}
public enum StatToBoost
{
    damage,
    defence,
    HP,
    mana,
    critical,
    parry
}

namespace InventorySystem
{

    public class ItemData : Item
    {
        public float dodgeChance;
        public float HP;
        public float mana;
        public float critical;
    }

    public class PlayerStats : MonoBehaviour
    {
        #region TextBox References
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
        #endregion

        ItemData overAllStats;
        ItemData tempOverAllStats;
        [SerializeField]
        InventoryData equipments;

        EquippedGears previewEquipments;

        private void OnEnable()
        {
            EquipmentsUpdated();
        }

        public void EquipmentsUpdated()
        {
            overAllStats = new ItemData();
            GetCurrentValues(equipments.gears, overAllStats);
            CalculateAdditionalStats(overAllStats);
            CalculateMythicalBoost(equipments.gears, overAllStats);
            UpdateUIValues();
        }

        public void OnSelectedItem(ItemUI item)
        {
            PreviewInfo(CalculatePreviewValues(item.itemInfo));
        }
        
        public void ItemEquipped(ItemAndSlot item)
        {
            if (item == null)
                return;
            AddStats(item.item, overAllStats);
            CalculateAdditionalStats(overAllStats);
            CalculateMythicalBoost(equipments.gears, overAllStats);
            UpdateUIValues();
        }
        public void ItemDequipped(ItemAndSlot item)
        {
            if (item == null)
                return;
            SubstractStatsForItem(item.item, overAllStats);
            CalculateAdditionalStats(overAllStats);
            CalculateMythicalBoost(equipments.gears, overAllStats);
            UpdateUIValues();
        }


        ItemData CalculatePreviewValues(Item item = null)
        {
            previewEquipments = equipments.gears.GetClone();

            //GetCurrentValues(tempOverAllStats);
            if (item!=null)
            {
                switch (item.slot)              //switch corrosponding item with selected item
                {
                    case ItemSlot.Weapon:
                        if (equipments.IsWeaponAlreadyEquipped(item.item_name) == 0)
                        {
                            if (equipments.isItemEquipped(item))
                                previewEquipments.weapon2Gear = null;
                            else
                                previewEquipments.weapon2Gear = item;
                        }
                        else
                        {
                            if (equipments.isItemEquipped(item))
                                previewEquipments.weapon1Gear = null;
                            else
                                previewEquipments.weapon1Gear = item;   
                        }
                        break;
                    case ItemSlot.Head:
                        if (equipments.isItemEquipped(item))
                            previewEquipments.headGear = null;
                        else
                            previewEquipments.headGear = item;
                        break;
                    case ItemSlot.Body:
                        if (equipments.isItemEquipped(item))
                            previewEquipments.bodyGear = null;
                        else
                            previewEquipments.bodyGear = item;
                        break;
                    case ItemSlot.Feet:
                        if (equipments.isItemEquipped(item))
                            previewEquipments.LegsGear = null;
                        else
                            previewEquipments.LegsGear = item;
                        break;
                    default:
                        break;
                }
            }
            else
                return null;

            tempOverAllStats = new ItemData();
            GetCurrentValues(previewEquipments, tempOverAllStats);

            CalculateAdditionalStats(tempOverAllStats);
            CalculateMythicalBoost(previewEquipments,tempOverAllStats);
            return tempOverAllStats;
        }

        void CalculateMythicalBoost(EquippedGears gears, ItemData stats)
        {
            ItemData boostStats = new ItemData();
            if (gears.weapon1Gear!=null  && gears.weapon1Gear.item_class == ItemClass.Mythical)
                GetBoostedStats(boostStats, stats, gears.weapon1Gear.mythicalBoost);

            if(gears.weapon2Gear != null && gears.weapon2Gear.item_class == ItemClass.Mythical)
                GetBoostedStats(boostStats, stats, gears.weapon2Gear.mythicalBoost);

            if(gears.bodyGear != null && gears.bodyGear.item_class == ItemClass.Mythical)
                GetBoostedStats(boostStats, stats, gears.bodyGear.mythicalBoost);

            if(gears.headGear != null && gears.headGear.item_class == ItemClass.Mythical)
                GetBoostedStats(boostStats, stats, gears.headGear.mythicalBoost);

            if(gears.LegsGear != null && gears.LegsGear.item_class == ItemClass.Mythical)
                GetBoostedStats(boostStats, stats, gears.LegsGear.mythicalBoost);

            AddStatsInfo(stats, boostStats);
            
        }

        void AddStatsInfo(ItemData stats,ItemData stats2)
        {
            stats.damage += stats2.damage;
            stats.dodgeChance += stats2.dodgeChance;
            stats.defence += stats2.defence;
            stats.critical += stats2.critical;
            stats.HP += stats2.HP;
            stats.mana += stats2.mana;
        }

        void GetBoostedStats(ItemData boostStats,ItemData baseStats, MythicalBoost boost)
        {
            switch (boost.boostsThis)
            {
                case StatToBoost.damage:
                    boostStats.damage += (baseStats.damage * boost.byPercentage/100);
                    break;
                case StatToBoost.defence:
                    boostStats.defence += (baseStats.defence * boost.byPercentage/100);
                    break;
                case StatToBoost.HP:
                    boostStats.HP += (baseStats.HP * boost.byPercentage/100);
                    break;
                case StatToBoost.mana:
                    boostStats.mana += (baseStats.mana * boost.byPercentage/100);
                    break;
                case StatToBoost.critical:
                    boostStats.critical +=(baseStats.critical * boost.byPercentage/100);
                    break;
                case StatToBoost.parry:
                    boostStats.dodgeChance += (baseStats.dodgeChance * boost.byPercentage/100);
                    break;
                default:
                    break;
            }
        }

        void CalculateAdditionalStats(ItemData data)
        {
            data.critical = data.agility * 0.15f;
            data.dodgeChance = data.agility * 0.2f;
            data.HP = data.strength * 12;
            data.mana= data.intel * 14;
        }

        void GetCurrentValues(EquippedGears gears, ItemData stats)
        {
            AddStats(gears.headGear, stats);
            AddStats(gears.bodyGear, stats);
            AddStats(gears.LegsGear, stats);
            AddStats(gears.weapon1Gear, stats);
            AddStats(gears.weapon2Gear, stats);
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

        void PreviewInfo(ItemData item)
        {
            if (item!=null)
            {
                SetPreviewString(damageText, overAllStats.damage, item.damage);
                SetPreviewString(defenceText, overAllStats.defence, item.defence);
                SetPreviewString(AGIText, overAllStats.agility, item.agility);
                SetPreviewString(INTText, overAllStats.intel, item.intel);
                SetPreviewString(STRText, overAllStats.strength, item.strength);
                SetPreviewString(critText, overAllStats.critical, item.critical);
                SetPreviewString(manaText, overAllStats.mana, item.mana);
                SetPreviewString(HPText, overAllStats.HP, item.HP);
                SetPreviewString(dodgeText, overAllStats.dodgeChance, item.dodgeChance); 
            }
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

        void UpdateUIValues()
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
    }

}