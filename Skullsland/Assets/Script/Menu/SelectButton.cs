using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    public Transform transformParent;
    public int MaxIndex;                       //numero total de buttons
    public static int index;                   //numero em que está sendo usado
    private float maxtime = .2f;
    private float time;

    public ButtonController[] buttonController;
    // Start is called before the first frame update
    void Start()
    {
        buttonController = transformParent.GetComponentsInChildren<ButtonController>();
        MaxIndex = buttonController.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        

        ChooseButton();


    }

    public void ChooseButton()
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
    public void PressButton()
    {

    }
}
