using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    public int IdButton;

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

    [Header("Size Button")]
    public Vector3 sizeBig;
    private Vector3 sizeNormal = new Vector3(1, 1, 1);

    public void ButtonStart()
    {
        SceneManager.LoadScene(nameScene);
    }
    public void ButtonOptions()
    {
        GameController.HaveEsc = false;
        Disable.SetActive(false);
        Activate.SetActive(true);
    }
    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonSizeNormal()
    {
        transform.localScale = sizeNormal;
    }
    public void ButtonSizeModify()
    {
        transform.localScale = sizeBig;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Active)
        {
            GameController.HaveEsc = true;
            Disable.SetActive(false);
            Activate.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonSizeNormal();

        if (start)
        {
            ButtonStart();
        }else if (options)
        {
            ButtonOptions();
        }
        else if (quit)
        {
            ButtonQuit();
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSizeModify();

        SelectButton.index = IdButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonSizeNormal();
    }

   
}
