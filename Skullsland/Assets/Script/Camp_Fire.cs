using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp_Fire : MonoBehaviour
{
    [Header("Stats")]
    public int Maxlife;
    private int life;
    public int HeatResgen;
    public bool was_attacked;
    public bool InRadius;
    [Header("Info")]
    public float range;
    public LayerMask layerplayer;
    [Header("TimeRegen")]
    public float TimeResgenLife;
    private float T_R_L;
    public float TimeRegenHeat;
    private float T_R_H;
    public float reset_time_attacked; // reseta o tempo em que foi atacado
    private float R_T_A;
    public static bool InRadiusCampFire;


    // Start is called before the first frame update
    void Start()
    {
        life = Maxlife;
        T_R_L = TimeResgenLife;
        T_R_H = TimeRegenHeat;
        R_T_A = reset_time_attacked;
    }
    public void TakeDamage(int dmg)
    {
        life -= dmg;
        was_attacked = true;
    }
    private void Update()
    {
        if (VerifyLife())
        {
            Destroy(gameObject);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(2);

            }

            Collider[] hitInfo = Physics.OverlapSphere(transform.position, 5);

            //confere se o player está no raio de attack
            InRadius = Physics.CheckSphere(transform.position, range, layerplayer);
            InRadiusCampFire = InRadius;
      
            if (InRadius)
            {
                Debug.Log("no raio do player");

                if (RegenHeat())
                {
                    Debug.Log("ganhando calor");
                    GameController.instance.AddHeat(HeatResgen);
                }
            }

            if (Increase_life())
            {
                if (!was_attacked)
                {
                    if (RegenLife())
                    {
                        Debug.Log("ganhando vida");
                        life++;
                    }
                }
                else
                {
                    // reseta o tempo em que foi atacado
                    ResetWasAttacked();
                }
            }
        }
       
    }
    public void ResetWasAttacked()
    {
        if (R_T_A < 0)
        {
            was_attacked = false;
            R_T_A = reset_time_attacked;
            T_R_L = TimeResgenLife;
        }
        else
        {
            R_T_A -= Time.deltaTime;
        }
    }
    bool Increase_life()
    {
        if (life < Maxlife)
        {

            return true;
        }
        else
        {
            life = Maxlife;
            return false;
        }

    }
    bool RegenLife()
    {
        if(T_R_L < 0)
        {
            T_R_L = TimeResgenLife;
            return true;
        }
        else
        {
            T_R_L -= Time.deltaTime;
            return false;
        }

    }
    bool RegenHeat()
    {
        if (T_R_H < 0)
        {
            T_R_H = TimeRegenHeat;
            return true;
        }
        else
        {
            T_R_H -= Time.deltaTime;
            return false;
        }

    }
    bool VerifyLife()
    {
        if (life <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
