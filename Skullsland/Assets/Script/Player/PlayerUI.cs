using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    [Header("Image Bar")]
    public Image I_barLife;
    public Image I_barEnergy;
    public Image I_barThrist;
    public Image I_barFood;

    [Header("Text Bar")]

    public TextMeshProUGUI T_barLife;
    public TextMeshProUGUI T_barEnergy;
    public TextMeshProUGUI T_barThrist;
    public TextMeshProUGUI T_barFood;

    private PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Mais de uma instancia encontrada");
            return;
        }
        instance = this;

     

        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerStats.life--;
        }
       

       
      
       
    }
    void UpdateBar()
    {
        BarUpdate(I_barLife,T_barLife, PlayerStats.life, stats.maxlife);
        BarUpdate(I_barEnergy,T_barEnergy, PlayerStats.energy, stats.maxenergy);
        BarUpdate(I_barFood,T_barFood, PlayerStats.food, stats.maxfood);
        BarUpdate(I_barThrist,T_barThrist, PlayerStats.thirst, stats.maxthirst);
        
    }
   
    public  void UpdateUI()
    {
        if (PlayerStats.life >= stats.maxlife)
        {
            PlayerStats.life = stats.maxlife;
        }
        if (PlayerStats.energy >= stats.maxenergy)
        {
            PlayerStats.energy = stats.maxenergy;
        }
        if (PlayerStats.thirst >= stats.maxthirst)
        {
            PlayerStats.thirst = stats.maxthirst;
        }
        if (PlayerStats.food >= stats.maxfood)
        {
            PlayerStats.food = stats.maxfood;
        }
        UpdateBar();
    }

    void BarUpdate(Image bar,TextMeshProUGUI text,float min,float max)
    {
        bar.fillAmount = min / max;
        text.SetText(min+"/"+max);
    }
}
