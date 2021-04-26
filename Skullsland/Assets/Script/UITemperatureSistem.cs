using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UITemperatureSistem : MonoBehaviour
{
    public Image UI_bartemp;

    public void TempBarSitem(float min, float max)
    {
        UI_bartemp.fillAmount = min / max;
    }
}
