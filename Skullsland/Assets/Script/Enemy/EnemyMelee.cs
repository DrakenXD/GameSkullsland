using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyController
{
    public bool AttackAuto;

    [Header("SFX")]
    public AudioSource SFX_attack;
    public AudioSource SFX_takedamage;
    public AudioSource SFX_Death;
    public AudioSource SFX_Idle;
    private float TimeSongIdle;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Idle()
    {
        base.Idle();
        anim.Play("Idle");

        if (TimeSongIdle <= 0)
        {
            SFX_Idle.Play();
            TimeSongIdle = 2f;
        }
        else TimeSongIdle -= Time.deltaTime;
    }

    public override void Death()
    {
        base.Death();
    
        anim.Play("Death");
    }
    public void DeathSong()
    {
        SFX_Death.Play();
    }
    public override void Attack()
    {
        if (AttackAuto)
        {
            base.Attack();
            nav.SetDestination(transform.position);
        }
      
    }
  
    public override void FollowTarget()
    {
        
        base.FollowTarget();
        anim.Play("Walking");

       

    }
   
    public void AttackAnim()
    {
      
        Collider[] hit = Physics.OverlapSphere(AttackPoint.position, radiusAttack, layerAttack);

        foreach (Collider hitPlayer in hit)
        {
            if (hitPlayer.CompareTag("Player"))
            {
                hitPlayer.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
        
    }
    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        SFX_takedamage.Play();
    }
    public void AttackCancelAnim()
    {
        Attaking = false;
    }
    public void ActiveSongAttack()
    {
        SFX_attack.Play();
    }
    public override void LoadingAttack()
    {
        
        base.LoadingAttack();
        anim.Play("Attack");
       
    }
   

    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(AttackPoint.position, MaxRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, MinRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, radiusAttack);
    }

}
