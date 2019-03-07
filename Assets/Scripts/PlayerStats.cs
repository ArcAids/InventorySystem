using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    float damage;
    float defence;
    float STR;
    float AGI;
    float INT;
    float HP;
    float mana;
    float crit;
    float dodge;
    
    public void PreviewInfo(ItemData item)
    {
        //itemBackground.color =;
        damageText.text =   GetPreviewString(damage,item.damage);
        defenceText.text =  GetPreviewString(defence,item.defence);
        AGIText.text =      GetPreviewString(AGI,item.agility);
        INTText.text =      GetPreviewString(INT,item.intel);
        STRText.text =      GetPreviewString(STR,item.strength);
        critText.text =     GetPreviewString(crit,item.critical);
        manaText.text =     GetPreviewString(mana,item.mana);
        HPText.text =       GetPreviewString(HP,item.HP);
        dodgeText.text =    GetPreviewString(dodge, item.dodgeChance);
    }

    string GetPreviewString(float currentValue, float additionalValue)
    {
        float difference = currentValue - additionalValue;
        string show = currentValue+" ";
        show += difference > 0 ? "+ " + difference : difference < 0? "- " + difference:"";
        return show;
    }

    public void UpdateValues(ItemData item)
    {
        damage=item.damage;
        defence=item.defence;
        STR=item.strength;
        AGI=item.agility;
        INT=item.intel;
        HP=item.HP;
        mana=item.mana;
        crit=item.critical;
        dodge=item.dodgeChance;

        damageText.text = damage + "";
        defenceText.text = defence + "";
        AGIText.text = AGI + "";
        INTText.text =INT + "";
        STRText.text = STR + "";
        critText.text =crit + "";
        manaText.text =mana + "";
        HPText.text =HP + "";
        dodgeText.text = dodge + "";
    }
}
