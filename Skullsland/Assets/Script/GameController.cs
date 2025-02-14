﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameController : MonoBehaviour
{
    [Header("Days")]
    public int DAYSTOTAL;
    public int days;
    public int dayForWinter;
    public int WinterDays;
    public bool winter;
    public int dayForSummer;
    public int SummerDays;
    public bool summer;
    private bool count;

    [Header("Max Graus in season + And -")]
    public int S_GrausPositive;
    public int S_GrausNegative;

    [Header("Max Graus day normal  + And -")]
    public int N_GrausPositive;
    public int N_GrausNegative;

    [Header("Penalty in Cold & Heat")]
    public float C_speed;
    public float C_speerun;
    public int H_useofwater;



    [Header("Time to restore life")]
    public int AmountRestoreLife;
    public float TimeRestoreLife;
    private float T_R_L;

    [Header("Lack of food and water")]
    public float TimeToLoseLife;
    private float T_T_Lose_Life;

    [Header("Restore Energy Move Or Stop")]
    public int M_LostAmountEnergy;
    public int S_AmountRestoredEnergy;

    [Header("Time Player Moving")]
    public float M_timefood;
    private float M_TF;
    public float M_timethirst;
    private float M_TT;
    public float M_timeEnergy;
    private float M_TE;

    [Header("Time Player Stopped")]
    public float S_timefood;
    private float S_TF;
    public float S_timethirst;
    private float S_TT;
    public float S_timeEnergy;
    private float S_TE;

    [Header("Hour")]
    public Transform HourPointer;

    [Header("UI Temperature")]
    public Image UI_bartemp;
    public Image UI_bartempCold;
    public Image UI_bartempHot;
    public CanvasGroup EffectCold;
    public CanvasGroup EffectHot;

    int T_rdValue;
    bool T_updatevalue;
    public float T_timerupdate;

    [Header("Esc options")]
    public GameObject esc;
    public bool activate = false;

    public static bool usingController;
    private TGSky tgsky;
    private PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
    

        tgsky = GameObject.FindGameObjectWithTag("Sun").GetComponent<TGSky>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        //movendo
        M_TF = M_timefood;
        M_TT = M_timethirst;
        M_TE = M_timeEnergy;

        //parado
        S_TF = S_timefood;
        S_TT = S_timethirst;
        S_TE = S_timeEnergy;

        T_T_Lose_Life = TimeToLoseLife;
    }

    // Update is called once per frame
    void Update()
    {
        Hour();
        MouseOrController();
        TimeUpdate();
       

    }
    
    void TimeUpdate()
    {
        Life();
        Energy();

        if (M_TF <= 0)
        {

            PlayerStats.instance.food--;
            

            M_TF = M_timefood;

        }
        if (M_TT <= 0)
        {

            if (!summer)
            {
                PlayerStats.instance.thirst--;
            }
            else
            {
                PlayerStats.instance.thirst -= H_useofwater;
            }
            

            


            M_TT = M_timethirst;

        }
       

        if (S_TF <= 0)
        {

            PlayerStats.instance.food--;


            S_TF = S_timefood;

        }
        if (S_TT <= 0)
        {

            PlayerStats.instance.thirst--;


            S_TT = S_timethirst;

        }
      

        //contando o tempo enquanto se move
        if (PlayerController.IsWalking )
        {

            M_TF -= Time.deltaTime;
            M_TT -= Time.deltaTime;
            M_TE -= Time.deltaTime;

        }
        else if (PlayerController.IsRunning)
        {
            M_TF -= Time.deltaTime * 1.5f;
            M_TT -= Time.deltaTime * 1.5f;
            M_TE -= 0.2f;

        }
        else
        {
            S_TF -= Time.deltaTime;
            S_TT -= Time.deltaTime;
            S_TE -= Time.deltaTime;
        }


        PlayerUI.instance.UpdateUI();
        
        Temperature();

    }
    private void Life()
    {
        

        if (PlayerStats.instance.food <= 0 || PlayerStats.instance.thirst <= 0)
        {
            if (T_T_Lose_Life<=0)
            {
                PlayerStats.instance.life--;
                T_T_Lose_Life = TimeToLoseLife;
            }
            else
            {
                T_T_Lose_Life -= Time.deltaTime;
            }
        }
        else
        {
            if (PlayerStats.instance.life < stats.maxlife)
            {
                if (T_R_L <= 0)
                {
                    PlayerStats.instance.life += AmountRestoreLife;

                    PlayerStats.instance.food--;
                    PlayerStats.instance.thirst--;

                    T_R_L = TimeRestoreLife;
                }
                else
                {
                    T_R_L -= Time.deltaTime;
                }
            }
        }
    }
    private void Energy()
    {
        if (M_TE <= 0 && PlayerStats.instance.food > 0 && PlayerStats.instance.thirst > 0)
        {
            PlayerStats.instance.energy -= M_LostAmountEnergy;

            M_TE = M_timeEnergy;
        }
        if (S_TE <= 0 && PlayerStats.instance.food > 0 && PlayerStats.instance.thirst > 0)
        {
            PlayerStats.instance.energy += S_AmountRestoredEnergy;

            S_TE = S_timeEnergy;
        }
    }
    private void Timer()
    {

        if (T_timerupdate > 0) 
        {
           
            T_timerupdate -= Time.deltaTime;
            
        }
        else 
        {
            T_rdValue = Random.Range(0, 101);
            T_updatevalue = true;
            T_timerupdate = tgsky.dayMinutesDuration;
            
        }

    }
    private void Temperature()
    {

        //contagem de graus
        if (T_updatevalue)
        {
            if (T_rdValue < 51 )
            {

                if (!summer && !winter)
                {
                    if (PlayerStats.instance.Graus > N_GrausPositive-1)
                    {
                        PlayerStats.instance.Graus = N_GrausPositive;
                    }
                    else
                    {
                        PlayerStats.instance.Graus++;
                    }
                }
                else if (!winter && summer)
                {
                    PlayerStats.instance.Graus += 2;
                }

                if (PlayerStats.instance.Graus > S_GrausPositive-1)
                {
                    PlayerStats.instance.Graus = S_GrausPositive;
                }
               
               

            }
            else if(T_rdValue > 51)
            {

                if (!winter && !summer)
                {
                    if (PlayerStats.instance.Graus < N_GrausNegative-1)
                    {
                        PlayerStats.instance.Graus = N_GrausNegative;
                    }
                    else
                    {
                        PlayerStats.instance.Graus--;
                    }
                }
                else if (winter && !summer)
                {
                    PlayerStats.instance.Graus -= 2;
                }
               
                if (PlayerStats.instance.Graus < S_GrausNegative+1)
                {
                    PlayerStats.instance.Graus = S_GrausNegative;
                }

                

            }

         


            T_updatevalue = false;
            
            
            if (PlayerStats.instance.Graus < PlayerStats.instance.maxheat && PlayerStats.instance.Graus > PlayerStats.instance.maxcold)
            {
                UI_bartempCold.enabled = false;
                UI_bartempHot.enabled = false;

                if (EffectCold.alpha > 0)
                {
                    EffectCold.alpha -= 0.050f;
                }
                else if (EffectHot.alpha > 0)
                {
                    EffectHot.alpha -= 0.050f;
                }

                UI_bartemp.color = Color.yellow;
            }

            //Desvantagens para o player ao passar do limite
            if (PlayerStats.instance.Graus > PlayerStats.instance.maxheat)
            {
                UI_bartempHot.enabled = true;
                UI_bartempCold.enabled = false;

                EffectHot.alpha += 0.050f;

                UI_bartemp.color = Color.red;

                if (EffectCold.alpha > 0)
                {
                    EffectCold.alpha -= 0.050f;
                }

                //ganha uma sede(ja colocado)

            }

            //Desvantagens para o player ao passar do limite
            if (PlayerStats.instance.Graus < PlayerStats.instance.maxcold)
            {
                UI_bartempCold.enabled = true;
                UI_bartempHot.enabled = false;

                EffectCold.alpha += 0.050f;

                UI_bartemp.color = Color.cyan;

                if (EffectHot.alpha > 0)
                {
                    EffectHot.alpha -= 0.050f;
                }

                PlayerStats.instance.speed = 5;
                PlayerStats.instance.speedrun = 8;
            }

            BarUpdate(UI_bartemp,PlayerStats.instance.Graus,100);

         
        }
        else
        {
            Timer();
        }
       
        //contagem de dias
        if (!TGSky.isNight)
        {

            


            if (count)
            {
                days++;
                
                DAYSTOTAL++;

                if (days >= dayForWinter && days <= WinterDays)
                {
                    summer = false;
                    winter = true;
                }
                else if (days >= dayForSummer && days <= SummerDays)
                {
                    winter = false;
                    summer = true;
                }
                else if (days > SummerDays)
                {

                    summer = false;
                    days = 0;
                }
                else if (days < WinterDays) 
                {
                    winter = false;
                }

                count = false;
            }

        }
        else
        {
            count = true;
        }

       
      
        
    }
    void BarUpdate(Image bar, float min, float max)
    {
        bar.fillAmount = min / max;
      
    }
    void Hour()
    { 
      
     
        float angle = (tgsky.hour/24)*360;
       

        HourPointer.localEulerAngles = new Vector3(0,0,-angle); 
    }
    public void MouseOrController()
    {
        Vector2 _rotMouse;
        Vector2 rotStick;


        _rotMouse.x = Input.GetAxisRaw("Mouse X");
        _rotMouse.y = Input.GetAxisRaw("Mouse Y");

        rotStick.x = Input.GetAxisRaw("Stick Right H");
        rotStick.y = -Input.GetAxisRaw("Stick Right V");

        if (rotStick.x<= -0.1f || rotStick.y >= 0.1f || rotStick.x <= -0.1f || rotStick.y >= 0.1f)
        {
          
            usingController = true;
        }
        if(_rotMouse.x <= -0.1f || _rotMouse.x >= 0.1f || _rotMouse.y <= -0.1f || _rotMouse.y >= 0.1f)
        {
            usingController = false;
          
        }
    }
    public void MudarATemporada(int day)
    {
        days = day;
    }
    void Esc()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !activate || Input.GetKeyDown(KeyCode.Joystick1Button7) && !activate)
        {
            activate = true;
            esc.SetActive(activate);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && activate || Input.GetKeyDown(KeyCode.Joystick1Button7) && activate)
        {
            activate = false;
            esc.SetActive(activate);
            Time.timeScale = 1;
        }
        
    }
    
}
