using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlots : MonoBehaviour
{
    public int stack;
    public int iDImage;
    Item item;
    public Image icon;
   
    public TextMeshProUGUI amount;
    public static int amountItem=0;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AddItem(Item newItem)
    {
        
        item = newItem;

        icon.sprite = newItem.icon;
        icon.enabled = true;
        

        amount.enabled = true;
        AmountItemStack();
    }
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

 
        amount.enabled = false;
    }
    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item,iDImage);
    }



    public void useItem()
    {
        if (item!=null )
        {
            if (item.itemtypes==ItemTypes.Consumable)
            {
                item.Use();
                Inventory.instance.RemoveItem(item,iDImage);
                AmountItemStack();
                if (amountItem == 0)
                {
                    ClearSlot();

                }
            }
            else
            {
                Debug.Log("n pode usar");
            }

           

        }
       
    }
    
  
    public void AmountItemStack()
    {
        stack = amountItem;
        Inventory.instance.QtdItems(iDImage);
        amount.SetText("" + amountItem);
    }
}
