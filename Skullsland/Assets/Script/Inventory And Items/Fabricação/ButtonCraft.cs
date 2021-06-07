using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCraft : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public int IdSlot;
    private MakingItem craft;

    private void Start()
    {
        craft = FindObjectOfType<MakingItem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        craft.CheckingItem(IdSlot);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.2f,1.2f,1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
