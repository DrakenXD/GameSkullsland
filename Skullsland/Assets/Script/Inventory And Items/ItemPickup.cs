using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public TextMeshProUGUI nameItem;
    public GameObject setUIName;
    public bool MouseUP;
    // Start is called before the first frame update
    void Start()
    {
        nameItem.SetText(item.name);
        transform.Rotate(0, Random.Range(-360, 360), 0);
    }
    private void Update()
    {
        ShowName();
    }
    void ShowName()
    {
        if (MouseUP)
        {
            setUIName.SetActive(true);
        }
        else
        {
            setUIName.SetActive(false);
        }
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
