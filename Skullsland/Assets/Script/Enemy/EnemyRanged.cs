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

        anim.SetBool("Attack", false);

    }
    public override void DeathAnim()
    {
        base.DeathAnim();
        Death();
    }
   

    public void Shoot()
    {
        
        NewBullet(bullet);
    }
  
    public override void LoadingAttack()
    {
        base.LoadingAttack();
        anim.SetBool("Attack",true);
    }
    public void NewBullet(GameObject _bullet)
    {
        Rigidbody rb = Instantiate(_bullet, pointShoot.position, Quaternion.identity).GetComponent<Rigidbody>();


        
        rb.AddForce(transform.forward*bulletvelocity, ForceMode.Impulse);
    }

    
    


}
