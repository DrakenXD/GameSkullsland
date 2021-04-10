using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int id;
    
    [Header("Max Stats")]
    public int maxlife;
    public int maxdamage;
    public float maxspeed;
    public float maxrun;
    public int maxfood;
    public int maxthirst;
    public int maxenergy;

    [Header("Update Stats")]
    public int life;
    public int damage;
    public float speed;
    public float run;
    public int food;
    public int thirst;
    public int energy;


    public static PlayerStats instance;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        life = maxlife;
        damage = maxdamage;
        speed = maxspeed;
        run = maxrun;
        food = maxfood;
        thirst = maxthirst;
        energy = maxenergy;
    }


}
