using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    public Transform transformParent;
    public bool menuOptions = false;
    public int MaxIndex;                       //numero total de buttons
    public static int index;                   //numero em que está sendo usado
    private float maxtime = .2f;
    private float time;

    public ButtonController[] buttonController;
    public ButtonPause[] buttonpause;
    // Start is called before the first frame update
    void Start()
    {
        if (!menuOptions)
        {
            buttonController = transformParent.GetComponentsInChildren<ButtonController>();
            MaxIndex = buttonController.Length - 1;
        }
        else if (menuOptions)
        {
            buttonpause = transformParent.GetComponentsInChildren<ButtonPause>();
            MaxIndex = buttonpause.Length - 1;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuOptions)
        {
            MenuChooseButton();
            MenuPressButton();
        }else if (menuOptions)
        {
            MenuOptionsPressButton();
            MenuOptionsChooseButton();
        }

        

    }

    public void MenuChooseButton()
    {
        if (time <= 0) 
        {
            if (-Input.GetAxisRaw("Vertical") >= 0.1f || Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttonController[index].ButtonSizeNormal();

                index++;
                if (index > MaxIndex)
                {
                    index = 0;
                    buttonController[index].ButtonSizeModify();
                }
                else
                {

                    buttonController[index].ButtonSizeModify();
                }

                time = maxtime;
            }
            else if(-Input.GetAxisRaw("Vertical") <= -0.1f || Input.GetKeyDown(KeyCode.DownArrow))
            { 
                buttonController[index].ButtonSizeNormal();

                index--;
                if (index < 0)
                {
                    index = MaxIndex;
                    buttonController[index].ButtonSizeModify();
                }
                else
                {

                    buttonController[index].ButtonSizeModify();
                }

                time = maxtime;
            }

            

        }else time -= Time.deltaTime;     
    }
    public void MenuPressButton()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            buttonController[index].ButtonClick();
        }
    }

    public void MenuOptionsChooseButton()
    {
        if (time <= 0)
        {
            if (-Input.GetAxisRaw("Vertical") >= 0.1f || Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttonpause[index].ButtonSizeNormal();

                index++;
                if (index > MaxIndex)
                {
                    index = 0;
                    buttonpause[index].ButtonSizeModify();
                }
                else
                {

                    buttonpause[index].ButtonSizeModify();
                }

                time = maxtime;
            }
            else if (-Input.GetAxisRaw("Vertical") <= -0.1f || Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttonpause[index].ButtonSizeNormal();

                index--;
                if (index < 0)
                {
                    index = MaxIndex;
                    buttonpause[index].ButtonSizeModify();
                }
                else
                {

                    buttonpause[index].ButtonSizeModify();
                }

                time = maxtime;
            }



        }
        else time -= Time.deltaTime;
    }
    public void MenuOptionsPressButton()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            buttonpause[index].ButtonClick();
        }
    }
}
