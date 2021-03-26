using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement pm;
    AudioSource lukeAudio;

    public float bounceForce;
    public AudioClip dieSFX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        lukeAudio = GetComponent<AudioSource>();

        if (bounceForce <= 0)
        {
            bounceForce = 20.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            lukeAudio.clip = dieSFX;
            GameManager.instance.lives--;
            lukeAudio.Play();
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.tag == "Enemy")
        {
            lukeAudio.clip = dieSFX;
            GameManager.instance.lives--;
            lukeAudio.Play();
            //if lives are > 0 - respawn and continue the level
        }
    }
}