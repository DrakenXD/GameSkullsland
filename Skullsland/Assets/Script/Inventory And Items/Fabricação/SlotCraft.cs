using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SlotCraft : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int IdButton;
    public TextMeshProUGUI TxtName;

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

    [Header("Size Button")]
    public Transform icon;
    public Vector3 sizeBig;
    private Vector3 sizeNormal = new Vector3(1, 1, 1);

    public void AddItemSlot(Craft craft)
    {
        item1 = craft.item1;
        item2 = craft.item2;
        itemresult = craft.itemResult;

        TxtName.SetText(""+craft.Name);

        amountItem1.SetText("" + craft.amountItem1);
        amountItem2.SetText("" + craft.amountItem2);
        amountItemResult.SetText("" + craft.amountItemResult);

        imageItem1.sprite = craft.item1.icon;
        imageItem2.sprite = craft.item2.icon;
        imageresult.sprite=craft.itemResult.icon;
    }

    public void ButtonSizeNormal()
    {
        icon.localScale = sizeNormal;
    }
    public void ButtonSizeModify()
    {
        icon.localScale = sizeBig;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSizeModify();

        SelectButton.index = IdButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonSizeNormal();
    }
}
