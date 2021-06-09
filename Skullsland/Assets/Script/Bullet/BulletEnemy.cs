using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int damage;
    private bool isground;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            damage = 0;
            isground = true;
        }
        if (collision.gameObject.CompareTag("Player") && !isground)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
