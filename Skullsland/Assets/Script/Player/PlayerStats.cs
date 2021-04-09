using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int id;
    [Header("Stats Player")]
    public int maxlife;
    public int maxdamage;
    public float maxspeed;
    public int maxfood;
    public int maxthirst;
    public int maxenergy;

    public static int life;
    public static int damage;
    public static float speed;
    public static int food;
    public static int thirst;
    public static int energy;


  
    private void Awake()
    {
        life = maxlife;
        damage = maxdamage;
        speed = maxspeed;
        food = maxfood;
        thirst = maxthirst;
        energy = maxenergy;
    }


}
