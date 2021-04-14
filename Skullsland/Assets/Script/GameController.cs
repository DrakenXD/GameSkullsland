using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [Header("Time to restore life")]
    public int AmountRestoreLife;
    public float TimeRestoreLife;
    private float T_R_L;
    
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

    public static bool usingController;

    [Header("Hour")]
    public Transform ponteiro;
    public Image fill;
    private TGSky tgsky;
    private PlayerStats stats;
    public float test;
    [Header("Esc options")]
    public GameObject esc;
    public bool activate = false;
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

    }

    // Update is called once per frame
    void Update()
    {
        Hour();
        MouseOrController();
        TimeUpdate();
        PlayerUI.instance.UpdateUI();

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

            PlayerStats.instance.thirst--;


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
       


    }

    private void Life()
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
    void Hour()
    { 
        fill.fillAmount = tgsky.hour / 24;
     
        float angle = fill.fillAmount;
        
        ponteiro.Rotate(0,0,-angle);
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
