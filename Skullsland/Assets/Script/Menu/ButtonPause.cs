using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonPause : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public int IdButton;

    [Header("Resume")]
    public bool Resume;
    public GameObject MenuDisable;

    [Header("options")]
    public bool options;
    public GameObject Activate;
    public GameObject Disable;

    [Header("Menu")]
    public bool Menu;
    public string nameScene;

    [Header("Size Button")]
    public Vector3 sizeBig;
    private Vector3 sizeNormal = new Vector3(1, 1, 1);

    public void ButtonMenu()
    {
        SceneManager.LoadScene(nameScene);
    }
    public void ButtonOptions()
    {
        GameController.HaveEsc = false;
        Disable.SetActive(false);
        Activate.SetActive(true);
    }
    public void ButtonResume()
    {
        MenuDisable.SetActive(false);
        GameController.HaveEsc= true;
        Time.timeScale = 1f;
    }

    public void ButtonSizeNormal()
    {
        transform.localScale = sizeNormal;
    }
    public void ButtonSizeModify()
    {
        transform.localScale = sizeBig;
    }
    public void ButtonClick()
    {
        if (Menu)
        {
            ButtonMenu();
        }
        else if (options)
        {
            ButtonOptions();
        }
        else if (Resume)
        {
            ButtonResume();
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonSizeNormal();

        ButtonClick();

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
