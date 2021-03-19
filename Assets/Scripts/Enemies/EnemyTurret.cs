using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyTurret : MonoBehaviour
{

    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Transform Player;
    public Projectile projectilePrefab;

    public float projectileForce;
    public bool isFacingRight;

    public float projectileFireRate;
    float timeSinceLastFire = 0.0f;
    public int health;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (projectileForce <= 0)
        {
            projectileForce = 7.0f;
        }

        if (health <= 0)
        {
            health = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, GameManager.instance.playerInstance.transform.position);
        Debug.Log(Player.transform.position);

        if (transform.position.x < GameManager.instance.playerInstance.transform.position.x && !isFacingRight)
        {
            flip();
        }
        else if (transform.position.x > GameManager.instance.playerInstance.transform.position.x && isFacingRight)
        {
            flip();
        }

        if (Time.time >= timeSinceLastFire + projectileFireRate && distance <= 10)
        {
            anim.SetBool("Fire", true);
            timeSinceLastFire = Time.time;
        }

    }

    public void Fire()
    {
        if (isFacingRight)
        {
            Debug.Log("Fire Left Side");
            Projectile projectileInstance = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            projectileInstance.speed = projectileForce;
        }
        else
        {
            Debug.Log("Fire Right Side");
            Projectile projectileInstance = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            projectileInstance.speed = -projectileForce;
        }
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health--;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                GameManager.instance.score++;
                Destroy(gameObject);
            }
        }
    }
    void flip()
    {
        // Method 1: Toggle isFacingRight variable
        isFacingRight = !isFacingRight;

       
        Vector3 scaleFactor = transform.localScale;

        // Flip scale of 'x' variable
        scaleFactor.x *= -1;    // scaleFactor.x = -scaleFactor.x;

        // Update scale to new flipped value
        transform.localScale = scaleFactor;
    }
}