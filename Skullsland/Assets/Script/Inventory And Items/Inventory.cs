using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.Log("Mais de uma instancia encontrada");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 20;

    public List<Item> items = new List<Item>();
    public List<int> AmountItens = new List<int>();
 

    public bool AddItem(Item _item)
    {
        if (!_item.isDefaultItem)
        {
            if (items.Count>=space)
            {
                
                return false;
            }
            if (!items.Contains(_item))
            {

                items.Add(_item);
                AmountItens.Add(1);
            }
            else
            {
                int index = -1 ;
                
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] == _item )
                    {
                        if (AmountItens[i] < items[i].MaximunStacks)
                        {
                            AmountItens[i]++;
                            index = i;
                            break;
                        }
                       
                        
                    }
                   
                    
                    
                }

                if (index == -1) 
                {
                    items.Add(_item);
                    AmountItens.Add(1);
                }
            }
            

            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }
        return true;
    }
    
    public void RemoveItem(Item _item,int idSlot)
    {
        
        if (items.Contains(_item))
        {
            if (AmountItens[idSlot] > 0)
            {
                AmountItens[idSlot]--;
            }

            if (AmountItens[idSlot] == 0)
            {
                items.Remove(_item);
                AmountItens.Remove(AmountItens[idSlot]);
            }
        }
        else
        {
            Debug.Log(" não tem este item");
        }

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    public void RemoveItemCraft(Item _item, int amountRemove)
    {

        if (items.Contains(_item))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == _item)
                {
                    if (AmountItens[i] > 0)
                    {
                        AmountItens[i]-=amountRemove;
                    }

                    if (AmountItens[i] == 0)
                    {
                        items.Remove(_item);
                        AmountItens.Remove(AmountItens[i]);
                    }


                }

            }
                  
           
        }
        else
        {
            Debug.Log(" não tem este item");
        }

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    public void QtdItems(int id)
    {

        InventorySlots.amountItem = AmountItens[id];
    }
    
    
}
