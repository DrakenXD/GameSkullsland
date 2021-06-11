using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemysType;
    public Transform[] spawns;

    public int MaxAmountSpawn;
    private int AmountSpawn;
    public float timespawnEnemy;
    private float T_S_E;

    private TGSky tgsky;
    // Start is called before the first frame update
    void Start()
    {
        tgsky = GameObject.FindGameObjectWithTag("Sun").GetComponent<TGSky>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TGSky.isNight)
        {
            SpawnController();
        }
        else
        {
            AmountSpawn = 0;
        }
    }

    public void SpawnController()
    {
        if (AmountSpawn < MaxAmountSpawn)
        {
            if (T_S_E <= 0)
            {
                int iE = Random.Range(0, enemysType.Length);
                int iS = Random.Range(0, spawns.Length);

                Instantiate(enemysType[iE], spawns[iS].position, Quaternion.identity);

                T_S_E = timespawnEnemy;

                AmountSpawn++;
            }
            else T_S_E -= Time.deltaTime;
        }
    }
}
