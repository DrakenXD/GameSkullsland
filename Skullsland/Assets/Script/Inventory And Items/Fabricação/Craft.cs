using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft {
    [Header("Name")]
    public string Name;
    [Header("Item And Amount")]
    public Item item1;
    public int amountItem1;
    [Header("Item And Amount")]
    public Item item2;
    public int amountItem2;
    [Header("Item Result && Item Craft")]
    public Item itemResult;
    public int amountItemResult;
    public GameObject itemprefab;

}
