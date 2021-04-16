using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int ID;
    new public string name = "New Item";   // Nome do item
    public Sprite icon = null;             // Item icone
    public string description;

    public bool isDefaultItem = false;     
    
    [Range(1,99)]public int MaximunStacks;
    public ItemTypes itemtypes;
    
    
    public virtual void Use()
    {

        Debug.Log("use item");

    }
}
