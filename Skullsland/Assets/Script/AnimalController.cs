using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    [Header("Stats")]
    public float MaxLife;
    protected float Life;
    public float normalspeed;
    public float scaredspeed;
    [Header("State")]
    public AnimalState state;

    [Header("UI")]
    public Image UI_Life;
    public Image UI_Damage;
    public GameObject prefabtextDamage;
    public Transform TextDamagePos;

    [Header("Move Target")]
    public Transform target;
    public float MaxTimeNextTarget;
    public float MinTimeNextTarget;
    public float MaxTimeStop;
    public float MinTimeStop;
    protected float T_N_T;
    protected float T_S;


    protected NavMeshAgent navagent;
    private bool was_attacked;

    // Start is called before the first frame update
    void Start()
    {
        Life = MaxLife;
        T_N_T = MaxTimeNextTarget;

        navagent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (was_attacked) UIDamageSystem();

        StateController();
     
    }
    public virtual void VerifyState()
    {

        switch (state)
        {
            case AnimalState.Idle:
                //animal parado
                FollowTarget(transform);
                break;
            case AnimalState.Follow:
                //animal seguindo o target
                FollowTarget(target);
                break;
        }
    }
    public void StateController()
    {
        float timenextrandom = Random.Range(MinTimeNextTarget, MaxTimeNextTarget);
        float timestop = Random.Range(MinTimeStop, MaxTimeStop);

        if (IsAlive())
        {
            Death();
        }
        else
        {
            if (T_S <= 0)
            {
                if (T_N_T <= 0)
                {
                    //teleporta o target para um local aleátorio
                    target.position = new Vector3(Random.Range(transform.position.x - 4f, transform.position.x + 4f),
                        transform.position.y, Random.Range(transform.position.z - 4f, transform.position.z + 4f));

                    T_N_T = timenextrandom;
                    T_S = timestop;
                }
                else
                {
                    state = AnimalState.Follow;
                    T_N_T -= Time.deltaTime;
                }
            }
            else
            {

                state = AnimalState.Idle;
                T_S -= Time.deltaTime;
            }
        }

        



        VerifyState();
    }
    protected void Death()
    {
        Destroy(gameObject,1f);
    }
    private bool IsAlive()
    {
        if (Life <= 0)
        {
            Life = 0;
            return true;
        }
        return false;
    }
    public void FollowTarget(Transform _target)
    {
        navagent.SetDestination(_target.position);    
    }
    public virtual void TakeDamage(int dmg)
    {

        GameObject txt = Instantiate(prefabtextDamage, TextDamagePos.position, Quaternion.identity);
        
        txt.GetComponent<TextMesh>().text = "" + dmg;

        Life -= dmg;

        UI_Life.fillAmount = Life / MaxLife;
        was_attacked = true;
    }

    public void UIDamageSystem()
    {
        if (UI_Life.fillAmount < UI_Damage.fillAmount)
        {
            UI_Damage.enabled = true;
            UI_Damage.fillAmount -= 0.015f;
        }
        else
        {
            UI_Damage.enabled = false;
            was_attacked = false;
            UI_Damage.fillAmount = UI_Life.fillAmount;
        }
    }
    public enum AnimalState
    {
        Idle,
        Follow,
        Death
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        
    }
}
