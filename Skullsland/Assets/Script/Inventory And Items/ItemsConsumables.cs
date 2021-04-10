using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumables")]
public class ItemsConsumables : Item
{
    [Header("Stats")]
    public int LifeRegen;
    public int FoodRegen;
    public int ThirstRegen;


    public override void Use()
    {
        base.Use();
        PlayerStats.instance.life += LifeRegen;
        PlayerStats.instance.food += FoodRegen;
        PlayerStats.instance.thirst += ThirstRegen;
        PlayerUI.instance.UpdateUI();
    }
}
