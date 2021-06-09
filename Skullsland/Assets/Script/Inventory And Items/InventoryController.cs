using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Transform itemsParent;
    public InventorySlots[] slots;
    Inventory inventory;

    public bool keydown;
    public int index=5;
    public int lastindex;
    public int maxindex;

    private Animator anim;
    public bool usingSlots;
    public float maxtime = 5f;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlots>();
        slots[0].GetComponent<Animator>().SetBool("Choose", true);
        maxindex = slots.Length - 1;


    }

    // Update is called once per frame
    void Update()
    {

        if (usingSlots)
        {
            anim.SetBool("Using", true);
            if (time <= 0)
            {
                usingSlots = false;
                anim.SetBool("Using", false);
                slots[index].CloseDescription();
                time = maxtime;
            }
            else
            {
                time -= Time.deltaTime;
                
            }
        }

        if (!MakingItem.activate)
        {
            ChooseSlotJoystick();
            UsingItemSlotJoystick();
            ChooseSlotKeyboardAndScroll();
            viewDescription();

            //HandWithObject.instance.ItemInHand(index);
        }
    }


    void viewDescription()
    {
        if (lastindex!=index )
        {
            slots[lastindex].CloseDescription();

            lastindex = index;

            slots[index].OpenDescription();
        }
       
       
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                anim.SetBool("Using", true);
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("updating ui");

        time = maxtime;
    }
    void ChooseSlotJoystick()
    {

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            usingSlots = true;
            time = maxtime;

            index++;
            if (index + 1 > slots.Length)
            {
                index = 0;
            }
            for (int i = 0; i < slots.Length; i++)
            {
                if (i == slots[index].iDImage)
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", true);
                }
                else
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", false);
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            usingSlots = true;
            time = maxtime;

            index--;
            if (index + 1 < 1)
            {
                index = slots.Length - 1;
            }
            for (int i = 0; i < slots.Length; i++)
            {
                if (i == slots[index].iDImage)
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", true);
                }
                else
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", false);
                }
            }
        }



    }
    private void UsingItemSlotJoystick()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            usingSlots = true;
            time = maxtime;

            slots[index].useItem();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            usingSlots = true;
            time = maxtime;

            slots[index].useItem();
        }

    }
    public void ChooseSlotMouse(int id)
    {
        usingSlots = true;
        time = maxtime;

        slots[index].GetComponent<Animator>().SetBool("Choose", false);

        slots[id].GetComponent<Animator>().SetBool("Choose", true);

        index = id;

    }
    private void ChooseSlotKeyboardAndScroll()
    {


        int _i = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 0;

            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 1;


            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 2;

            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 3;
            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 4;
            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 5;


            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 6;


            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _i = index;
            slots[_i].GetComponent<Animator>().SetBool("Choose", false);

            index = 7;


            usingSlots = true;
            time = maxtime;
            slots[index].GetComponent<Animator>().SetBool("Choose", true);
        }


        if (Input.GetAxis("Mouse ScrollWheel") <= -0.1f)
        {
            usingSlots = true;
            time = maxtime;

            index--;
            if (index + 1 < 1)
            {
                index = slots.Length - 1;
            }
            for (int i = 0; i < slots.Length; i++)
            {
                if (i == slots[index].iDImage)
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", true);
                }
                else
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", false);
                }
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") >= 0.1f)
        {
            usingSlots = true;
            time = maxtime;

            index++;
            if (index + 1 > slots.Length)
            {
                index = 0;
            }
            for (int i = 0; i < slots.Length; i++)
            {
                if (i == slots[index].iDImage)
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", true);
                }
                else
                {
                    slots[i].GetComponent<Animator>().SetBool("Choose", false);
                }
            }
        }

    }


    
}
