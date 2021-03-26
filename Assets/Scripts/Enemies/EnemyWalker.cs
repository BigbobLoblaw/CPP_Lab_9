using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyWalker : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    AudioSource walkerAudio;

    public int health;
    public float speed;
    public AudioClip dieSFX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        walkerAudio = GetComponent<AudioSource>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (health <= 0)
        {
            health = 3;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Death"))
        {
            if (sr.flipX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }

    public void IsDead()
    {
        health--;
        if (health <= 0)
        {
            walkerAudio.clip = dieSFX;
            anim.SetBool("Death", true);
            walkerAudio.Play();
            rb.velocity = Vector2.zero;
        }
    }

    public void FinishedDeath()
    {
        GameManager.instance.score++;
        Destroy(gameObject);
    }
}
