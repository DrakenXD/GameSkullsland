using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotCraft : MonoBehaviour
{
    [Header("Item 1")]
    private Item item1;
    public Image imageItem1;
    public TextMeshProUGUI amountItem1;
    [Header("Item 2")]
    private Item item2;   
    public Image imageItem2;
    public TextMeshProUGUI amountItem2;
    [Header("Item Result")]
    private Item itemresult;
    public Image imageresult;
    public TextMeshProUGUI amountItemResult;

    public void AddItemSlot(Craft craft)
    {
        item1 = craft.item1;
        item2 = craft.item2;
        itemresult = craft.itemResult;

        amountItem1.SetText("" + craft.amountItem1);
        amountItem2.SetText("" + craft.amountItem2);
        amountItemResult.SetText("" + craft.amountItemResult);

        imageItem1.sprite = craft.item1.icon;
        imageItem2.sprite = craft.item2.icon;
        imageresult.sprite=craft.itemResult.icon;
    }

   
}
