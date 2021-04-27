using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWithObject : MonoBehaviour
{
    public static HandWithObject instance;

    private InventoryController inventoryController;
    public GameObject ItemPrefab; //modelo do item
    public Transform pointItem;
    public int i =0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Mais de uma instancia encontrada");
            return;
        }
        instance = this;
        transform.parent = pointItem;
        inventoryController = GameObject.FindGameObjectWithTag("InventoryController").GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ItemInHand(int index)
    {
        if (inventoryController.index == index)
        {

            if (inventoryController.slots[index].prefabitem!=null)
            {
               
               
                
               
                if (ItemPrefab == inventoryController.slots[index].prefabitem)
                {
                    i = 0;
                    ItemPrefab.transform.position = pointItem.position;
                }
                else
                {
                    i = 1;
                    if (i==1)
                    {
                        ItemPrefab = inventoryController.slots[index].prefabitem;

                        Instantiate(ItemPrefab, transform.position, Quaternion.identity);

                        
                    }
                }

                
            }



        }
        else
        {
            Destroy(ItemPrefab);
        }
    }
}
