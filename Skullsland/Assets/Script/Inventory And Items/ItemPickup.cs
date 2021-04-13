using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, Random.Range(-360, 360), 0);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool wasPickedUp = Inventory.instance.AddItem(item);
            if (wasPickedUp)
                Destroy(gameObject);
        }
    }
    
  
}
