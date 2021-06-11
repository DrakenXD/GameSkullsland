using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonControllerInGame : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public int id;
    [Header("BackMenu")]
    public bool backMenu;
    public string nameScene;

  

    [Header("Options")]
    public bool CloseOptionsInEsc;
    public bool options;
    public GameObject Activate;
    public GameObject Disable;

    private Vector3 sizeNormal = new Vector3(1, 1, 1);
    [Header("Size Button")]
    public Vector3 sizeBig;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CloseOptionsInEsc)
        {
            Disable.SetActive(false);
            Activate.SetActive(true);
        }
    }


    public void ButtonSizeNormal()
    {
        transform.localScale = sizeNormal;
    }
    public void ButtonSizeModify()
    {
        transform.localScale = sizeBig;
    }
    public void EnterButton()
    {
        ButtonSizeNormal();

        if (backMenu)
        {
            SceneManager.LoadScene(nameScene);
        }
        else if (options)
        {

            Disable.SetActive(false);
            Activate.SetActive(true);

        }
    }
        

    public void OnPointerClick(PointerEventData eventData)
    {
        EnterButton();
      
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSizeModify();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonSizeNormal();
    }

}
