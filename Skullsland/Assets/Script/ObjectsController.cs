using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    [Header("Life")]
    public int life; //vida

    [Header("AmountDrop")]
    public int maxamountdrop;
    public int amountrandom;
    public int amount;

    [Header("Prefab item and pointDrop")]
    public GameObject prefabitem; //item que vai cair
    public Transform[] pointDrop; //local para cair os itens
    public int indexpointdrop;
    // Start is called before the first frame update
    void Start()
    {
        amountrandom = Random.Range(1, maxamountdrop);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLive())
        {
            AmountDrop();
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        
    }
    bool IsLive()
    {
        if (life <= 0) return true;
        return false;
        
    }
    public void AmountDrop()
    {

        if (amount<amountrandom)
        {
            if (indexpointdrop< pointDrop.Length-1)
            {
                indexpointdrop++;
            }
            else
            {
                indexpointdrop = 0;
            }


            Instantiate(prefabitem, pointDrop[indexpointdrop].position, Quaternion.identity);

            amount++;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
