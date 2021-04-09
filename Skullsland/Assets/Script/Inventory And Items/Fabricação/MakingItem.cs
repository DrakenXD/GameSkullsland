using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MakingItem : MonoBehaviour
{
    public GameObject ActivateCraftUI;
    public static bool activate = false;

    public Transform pointDrop;

    
    public List<Craft> craft = new List<Craft>();


    [Header("UI slots")]
    public Transform itemsParent;
    SlotCraft[] slots;

    public int index;
    public int maxindex;

    // Start is called before the first frame update
    void Start()
    {
       

        slots = itemsParent.GetComponentsInChildren<SlotCraft>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && !activate || Input.GetKeyDown(KeyCode.Joystick1Button3) && !activate)
        {
            activate = true;
            ActivateCraftUI.SetActive(true);
            
            UpdateUISlotCraft();
        }else if (Input.GetKeyDown(KeyCode.E) && activate || Input.GetKeyDown(KeyCode.Joystick1Button3) && activate)
        {
            activate = false;
            ActivateCraftUI.SetActive(false);

        }

    }

    private void UpdateUISlotCraft()
    {
        for (int i = 0; i < craft.Count; i++)
        {
            if (i < craft.Count)
            {
                slots[i].AddItemSlot(craft[i]);
            }
           
        }
        
    }

    public void CheckingItem(int idSlotCraft)
    {
        if (CheckAmountAndItem(craft[idSlotCraft].item1, craft[idSlotCraft].amountItem1) && CheckAmountAndItem(craft[idSlotCraft].item2, craft[idSlotCraft].amountItem2))
        {
            DropItem(idSlotCraft);
            Inventory.instance.RemoveItemCraft(craft[idSlotCraft].item1,craft[idSlotCraft].amountItem1);
            Inventory.instance.RemoveItemCraft(craft[idSlotCraft].item2,craft[idSlotCraft].amountItem2);
        }
        else
        {
            Debug.Log("Falta Item");
        }
        

    }

    private void DropItem(int idSlotCraft)
    {

        for (int i = 0; i < craft[idSlotCraft].amountItemResult; ) 
        {
            Instantiate(craft[idSlotCraft].itemprefab, pointDrop.position, Quaternion.identity);
            i++;
        }
    }
    private bool CheckAmountAndItem(Item _item,int amount)
    {
        for (int i = 0; i < Inventory.instance.items.Count; i++)
        {
            if (Inventory.instance.items[i]==_item)
            {
                if (Inventory.instance.AmountItens[i] >= amount)
                {
                    return true;
                }
            }
            
        }
        return false;
    }
}
