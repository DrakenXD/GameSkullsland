using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour,IDropHandler, IPointerDownHandler,IBeginDragHandler,IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("inicio drag");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("fim drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Apertou");
    }

   
}
