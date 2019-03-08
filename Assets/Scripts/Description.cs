using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InventorySystem
{
    public class Description : MonoBehaviour
    {
        [SerializeField]
        PlayerEquipmentsUI playerEquipments;
        [SerializeField]
        TMP_Text itemName;
        [SerializeField]
        TMP_Text itemDescription;
        [SerializeField]
        Image itemIcon;
        [SerializeField]
        TMP_Text ItemClassName;
        [SerializeField]
        Image itemBackground;
        [SerializeField]
        TMP_Text damage;
        [SerializeField]
        TMP_Text defence;
        [SerializeField]
        TMP_Text STR;
        [SerializeField]
        TMP_Text AGI;
        [SerializeField]
        TMP_Text INT;
        Item item;

        public void UpdateInfo(Item item)
        {
            this.item = item;
            itemName.text = item.item_name;
            itemDescription.text = item.description;
            itemIcon.sprite = item.icon;
            ItemClassName.text = System.Enum.GetName(typeof(ItemClass), item.item_class);
            //itemBackground.color =;
            damage.text = item.damage + "";
            defence.text = item.defence + "";
            defence.text = item.defence + "";
            AGI.text = item.agility + "";
            INT.text = item.intel + "";
            STR.text = item.strength + "";
        }
    
        public void EquipDequip()
        {
            if (item!=null)
            {
                playerEquipments.EquipItem(item, true);
            }
        }
    }
}
