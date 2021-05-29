using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyController
{
    public bool AttackAuto;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void Attack()
    {
        if (AttackAuto)
        {
            base.Attack();
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
    public override void LoadingAttack()
    {
        base.LoadingAttack();
        anim.Play("Attack");
    }
    public override void DeathAnim()
    {
        anim.SetBool("IsDeath",true);
        

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
