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
    public float maxspeedrun;
    public int maxfood;
    public int maxthirst;
    public int maxenergy;
    

    [Header("Update Stats")]
    public int life;
    public int damage;
    public float speed;
    public float speedrun;
    public int food;
    public int thirst;
    public int energy;

    [Header("Temperature")]
    public int maxheat;
    public int maxcold;
    public int Graus;


    public static PlayerStats instance;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }

        life = maxlife;
        damage = maxdamage;
        speed = maxspeed;
        speedrun = maxspeedrun;
        food = maxfood;
        thirst = maxthirst;
        energy = maxenergy;
    }



}
