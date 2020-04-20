using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float bulletSpeed = 1;
    public float bulletDamage = 1;

    public void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed * 20, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EntitySkeleton>() != null)
        {
            other.GetComponent<EntitySkeleton>().Hurt(new Damage(bulletDamage, gameObject));
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Destroy(gameObject);
    }
}
