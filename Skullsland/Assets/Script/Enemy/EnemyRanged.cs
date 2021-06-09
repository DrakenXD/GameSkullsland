using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyController
{
    public GameObject bullet;
    public Transform pointShoot;
    public float bulletvelocity;

    public Vector2 m_force;

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
      
        

        nav.SetDestination(transform.position);

        
        
    }
    public override void FollowTarget()
    {
        base.FollowTarget();
        anim.Play("Walk");
    }
    public override void Death()
    {
        base.Death();

        anim.Play("Death");
    }


    public void Shoot()
    {
        NewBullet(bullet);
        anim.Play("State");
    }
  
    public override void LoadingAttack()
    {
        base.LoadingAttack();
        anim.Play("Attack");
    }
    public void NewBullet(GameObject _bullet)
    {
        GameObject Clone = Instantiate(_bullet, pointShoot.position, Quaternion.identity);

        Clone.GetComponent<BulletEnemy>().damage = damage;

        Clone.GetComponent<Rigidbody>().AddForce(transform.forward*bulletvelocity, ForceMode.Impulse);
    }

    
    


}
