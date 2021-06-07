using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockingCraft : MonoBehaviour
{
    public GameObject[] Craft;
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < Craft.Length; i++)
            {
                Craft[i].SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < Craft.Length; i++)
            {
                Craft[i].SetActive(false);
            }
        }
    }
}
