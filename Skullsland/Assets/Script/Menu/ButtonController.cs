using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    [Header("Start")]
    public bool start;
    public string nameScene;

    [Header("Options")]
    public bool Active;
    public bool options;
    public GameObject Activate;
    public GameObject Disable;

    [Header("Quit")]
    public bool quit;


    
    private Vector3 sizeNormal = new Vector3(1,1,1);
    [Header("Size Button")]
    public Vector3 sizeBig;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Active)
        {
            Disable.SetActive(false);
            Activate.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = sizeNormal;

        if (start)
        {
            SceneManager.LoadScene(nameScene);
        }else if (options)
        {
           
            Disable.SetActive(false);
            Activate.SetActive(true);

        }
        else if (quit)
        {
            Application.Quit();
        }


        Debug.Log("dsadas");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = sizeBig;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = sizeNormal;
    }

   
}
