using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
        {
            lifetime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Pickups")
        {
            if (collision.gameObject.tag == "Enemy" && gameObject.tag == "EnemyProjectile")
            {
                //do nothing
            }
            else if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyProjectile")
            {
                //do nothing
            }
            else
            {
                Debug.Log(collision.name);
                Destroy(gameObject);
            }
            
        }

        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "PlayerProjectile")
        {
            collision.gameObject.GetComponent<EnemyWalker>().IsDead();
            Destroy(gameObject);
        }

    }

}