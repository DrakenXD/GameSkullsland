using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public float maxLife;
    protected float Life;
    public int damage;

    [Header("Bar Image")]
    public Image UI_Life;

    [Header("PopUp")]
    public GameObject prefabtextdamage;

    [Header("Attack")]
    public float timeAttack;
    protected float T_A;
    public float radiusAttack;
    public LayerMask layerAttack;
    public bool InRadius;
   
    [Header("Follow Player")]
    public EnemyState state;
    public float distance;
    public float MaxRange;
    public float MinRange;


    [Header("Drop Item")]
    public GameObject[] items;
    public int MaxAmountDrop;
    public int amountdrop;
    protected int rdDrop;

    protected Animator anim;
    protected NavMeshAgent nav;
    protected Transform targetPlayer;
    protected Rigidbody rig;
    protected Collider coll;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Life = maxLife;
        T_A = timeAttack;

        rdDrop = Random.Range(1, MaxAmountDrop);

        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        rig = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // Update is called once per frame
    public virtual void Update()
    {
        ControlState();
    }

    public virtual void VerifyState()
    {
        switch (state)
        {
            case EnemyState.Stop:
                nav.SetDestination(transform.position);
                break;
            case EnemyState.Follow:
                FollowTarget();
                break;
            case EnemyState.Patrol:
                break;
            case EnemyState.Loadingattack:
                LoadingAttack();
                break;
            case EnemyState.Death:
                DeathAnim();
                nav.SetDestination(transform.position);
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }
    public virtual void LoadingAttack()
    {
        nav.SetDestination(transform.position);
        transform.LookAt(targetPlayer);
    }
    public virtual void ControlState()
    {
        //distancia do inimigo para o player
        distance = Vector3.Distance(transform.position, targetPlayer.position);

        //confere se o player está no raio de attack
        InRadius = Physics.CheckSphere(transform.position,radiusAttack,layerAttack);

        if (IsAlive())
        {
            state = EnemyState.Death;
        }
        else
        {
            if (InRadius)
            {
                if (T_A <= 0)
                {
                    T_A = timeAttack;
                    state = EnemyState.Attack;
                }
                else
                {
                    state = EnemyState.Loadingattack;
                    T_A -= Time.deltaTime;
                }
            }
            else
            {
                if (distance <= MaxRange)
                {
                    if (distance >= MinRange)
                    {
                        state = EnemyState.Follow;
                    }
                    else
                    {
                        state = EnemyState.Stop;
                    }

                }
                else
                {
                    state = EnemyState.Stop;
                }
            }
        }

        VerifyState();
    }
    public virtual void DeathAnim()
    {
        coll.enabled = false;
        rig.isKinematic = true;
    }
    public virtual void Death()
    {
        while (rdDrop > amountdrop)
        {
            amountdrop++;
            int rdDropItem = Random.Range(0, items.Length);

            GameObject clone = Instantiate(items[rdDropItem], new Vector3(transform.position.x,transform.position.y+2f,transform.position.z), Quaternion.identity);

            clone.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);

        }
        if (amountdrop == rdDrop)
        {
            Destroy(gameObject, 1.1f);
        }
    }
    private bool IsAlive()
    {
        if (Life<=0)
        {
            Life = 0;
            return true;
        }
        return false;
    }
    public virtual void Attack()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radiusAttack, layerAttack);

        foreach (Collider hitPlayer in hit)
        {
            if (hitPlayer.CompareTag("Player"))
            {
                hitPlayer.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
    }
    public virtual void TakeDamage(int dmg)
    {
        
        Life -= dmg;

        GameObject txt = Instantiate(prefabtextdamage, new Vector3(transform.position.x,2,transform.position.z), Quaternion.identity);
        txt.GetComponent<TextMesh>().text = ""+dmg;
        UI_Life.fillAmount = Life / maxLife;
    }
    
    public virtual void FollowTarget()
    {
        nav.SetDestination(targetPlayer.position);
    }
    public enum EnemyState
    {
        Stop,
        Patrol,
        Follow,
        Attack,
        Loadingattack,
        Death,
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,MaxRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MinRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }
}
