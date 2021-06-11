using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MakingItem : MonoBehaviour
{
    public static bool activate = false;
    public Animator anim;
    public Transform pointDrop;

    
    public List<Craft> craft = new List<Craft>();


    [Header("UI slots")]
    public Transform itemsParent;
    SlotCraft[] slots;

    public int index;
    public int MaxIndex;

    private float maxtime = .2f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
       

        slots = itemsParent.GetComponentsInChildren<SlotCraft>();

        MaxIndex = slots.Length - 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && !activate || Input.GetKeyDown(KeyCode.Joystick1Button3) && !activate)
        {
            anim.Play("Right");
            activate = true;
           
        }else if (Input.GetKeyDown(KeyCode.E) && activate || Input.GetKeyDown(KeyCode.Joystick1Button3) && activate)
        {
            anim.Play("Left");
            activate = false;

        }
        ChooseSlotJoystick();
        PressButton();

    }

    private void LateUpdate()
    {
        UpdateUISlotCraft();

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
    public void  ChooseSlotJoystick()
    {
        if (time <= 0)
        {
            if (Input.GetAxisRaw("Stick Right V") >= 0.1f || Input.GetKeyDown(KeyCode.UpArrow))
            {
                slots[index].ButtonSizeNormal();

                index++;
                if (index > MaxIndex)
                {
                    index = 0;
                    slots[index].ButtonSizeModify();
                }
                else
                {

                    slots[index].ButtonSizeModify();
                }

                time = maxtime;
            }
            else if (Input.GetAxisRaw("Stick Right V") <= -0.1f || Input.GetKeyDown(KeyCode.DownArrow))
            {
                slots[index].ButtonSizeNormal();

                index--;
                if (index < 0)
                {
                    index = MaxIndex;
                    slots[index].ButtonSizeModify();
                }
                else
                {

                    slots[index].ButtonSizeModify();
                }

                time = maxtime;
            }



        }
        else time -= Time.deltaTime;
    }
    public void PressButton()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            CheckingItem(index);
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
