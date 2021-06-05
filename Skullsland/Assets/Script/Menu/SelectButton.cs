using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    public Transform transformParent;
    public int MaxIndex;                       //numero total de buttons
    public static int index;                   //numero em que está sendo usado
    public int test;

    public ButtonController[] buttonController;
    // Start is called before the first frame update
    void Start()
    {
        MaxIndex = buttonController.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        

        ChooseButton();

        test = index;

    }

    public void ChooseButton()
    {

        if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.UpArrow))
        {
            buttonController[index].ButtonSizeNormal();

            if (index > MaxIndex)
            {
                index = 0;
            }else index++;

            buttonController[index].ButtonSizeModify();
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.DownArrow))
        {
            buttonController[index].ButtonSizeNormal();

            if (index < 0)
            {
                index = MaxIndex;
            }
            else index--;

            buttonController[index].ButtonSizeModify();
        }
    }
}
