using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int DAYSTOTAL;
    public int days;
    public int dayForWinter;
    public int WinterDays;
    public bool winter;
    public int dayForSummer;
    public int SummerDays;
    public bool summer;
    public bool count;

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

    [Header("Hour")]
    public Transform ponteiro;

    [Header("UI Temperature")]
    public TextMeshProUGUI txtTemperature;

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

    }

    // Update is called once per frame
    void Update()
    {
        Hour();
        MouseOrController();
        TimeUpdate();
       

    }
    private void LateUpdate()
    {
        PlayerUI.instance.UpdateUI();
        Temperature();
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
    private void Temperature()
    {
        if (!TGSky.isNight)
        {
            

            if (count)
            {
                days++;
                
                DAYSTOTAL = days;

                count=false;
            }

            if (days >= dayForWinter && days <= WinterDays)
            {
                summer = false;
                winter = true;
            }
            else if (days >= dayForSummer && days <= SummerDays) 
            {
                winter=false;
                summer = true;
            }

        }
        else
        {
            count = true;
        }

        //txtTemperature.SetText(PlayerStats.instance.Graus+"°");
    }
    void Hour()
    { 
      
     
        float angle = (tgsky.hour/24)*360;
       

        ponteiro.localEulerAngles = new Vector3(0,0,-angle); 
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
