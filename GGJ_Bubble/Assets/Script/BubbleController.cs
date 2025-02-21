using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D bubbleRigidbody;
    public CircleCollider2D bubbleCollider;


    private SpriteRenderer spriteRenderer;

    public bool frozen = false;
    private float frezeEndTime;

    public LayerMask wallLayer;

    public int lives = 3;
    public float bounceForce = 1f;

    public float[] colliderSizes = {0.5f,0.25f,0.1f};

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontr� un Animator en la burbuja.");
        }


        bubbleRigidbody = GetComponent<Rigidbody2D>();
        if (bubbleRigidbody == null)
        {
            Debug.LogError("No se encontr� un Rigidbody2D en la burbuja.");
        }


        bubbleCollider = GetComponent<CircleCollider2D>();
        if (bubbleCollider == null)
        {
            Debug.LogError("No se encontr� un CircleCollider2D en la burbuja.");
        }

        UpdateBubbleAnimationAndCollider();
    }

 

    private void Update()
    {
        if (frozen && Time.time > frezeEndTime)
        {
            DesactivateFreezePowerUp();
        }
    }
    public void ActivateFreezePowerUp(float duracion)
    {
        frozen = true;
        frezeEndTime = Time.time + duracion;
        animator.SetBool("Frezze", true);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Paredes"), true);
    }

    public void DesactivateFreezePowerUp()
    {
        frozen = false;
        animator.SetBool("Frezze", false);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Paredes"), false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            LoseLife();
            BounceAway(collision.contacts[0].normal);
        }
    }


    private void LoseLife()
    {
        lives--;
        if (lives > 0)
        {
            UpdateBubbleAnimationAndCollider();
            AudioManager.instance.PlaySFX(SoundType.BounceSFX, 0.8f);
        }
        else
        {
            //GameOVer
            AudioManager.instance.PlaySFX(SoundType.BurstSFX, 1f);
            Destroy(gameObject);
           
            WinLoseCondition.instance.LoseGame();
        }
    }
    private void UpdateBubbleAnimationAndCollider()
    {
        if (animator != null)
        {
            animator.SetInteger("Lives", lives);
        }
        else
        {
            Debug.Log("Animator no asignado en la burbuja");
        }


        if (bubbleCollider != null)
        {
            if (lives > 0 && lives <= colliderSizes.Length)
            {
                bubbleCollider.radius = colliderSizes[lives - 1];
            }
            else
            {
                Debug.LogWarning("El �ndice de 'colliderSizes' est� fuera de rango o no est� configurado.");
            }
        }
        else
        {
            Debug.LogError("CircleCollider2D no asignado en la burbuja.");
        }
    }
    private void BounceAway(Vector2 collisionNormal)
    {
        if (bubbleRigidbody != null)
        {
            bubbleRigidbody.velocity = Vector2.zero;

            // Aplicar una fuerza de rebote
            bubbleRigidbody.AddForce(collisionNormal * bounceForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("No se puede aplicar el rebote porque 'bubbleRigidbody' es null.");
        }
    }
    

}
