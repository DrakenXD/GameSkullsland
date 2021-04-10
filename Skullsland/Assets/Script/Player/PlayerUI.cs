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

 
    void UpdateBar()
    {
        BarUpdate(I_barLife,T_barLife, PlayerStats.instance.life, stats.maxlife);
        BarUpdate(I_barEnergy,T_barEnergy, PlayerStats.instance.energy, stats.maxenergy);
        BarUpdate(I_barFood,T_barFood, PlayerStats.instance.food, stats.maxfood);
        BarUpdate(I_barThrist,T_barThrist, PlayerStats.instance.thirst, stats.maxthirst);
        
    }
   
    public  void UpdateUI()
    {
        if (PlayerStats.instance.life >= stats.maxlife)
        {
            PlayerStats.instance.life = stats.maxlife;
        }
        if (PlayerStats.instance.energy >= stats.maxenergy)
        {
            PlayerStats.instance.energy = stats.maxenergy;
        }
        if (PlayerStats.instance.thirst >= stats.maxthirst)
        {
            PlayerStats.instance.thirst = stats.maxthirst;
        }
        if (PlayerStats.instance.food >= stats.maxfood)
        {
            PlayerStats.instance.food = stats.maxfood;
        }
        UpdateBar();
    }

    void BarUpdate(Image bar,TextMeshProUGUI text,float min,float max)
    {
        bar.fillAmount = min / max;
        text.SetText(min+"/"+max);
    }
}
