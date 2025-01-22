using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool frozen = false;
    private float frezeEndTime;

    public LayerMask wallLayer;

    public int lives = 3;
    public float bounceForce = 1f;

    private Animator animator;
    private Rigidbody2D bubbleRigidbody;
    private CircleCollider2D bubbleCollider;

    public float[] colliderSizes = {0.5f,0.25f,0.1f};

    private void Start()
    {
        originalColor = spriteRenderer.color;
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en la burbuja.");
        }


        bubbleRigidbody = GetComponent<Rigidbody2D>();
        if (bubbleRigidbody == null)
        {
            Debug.LogError("No se encontró un Rigidbody2D en la burbuja.");
        }


        bubbleCollider = GetComponent<CircleCollider2D>();
        if (bubbleCollider == null)
        {
            Debug.LogError("No se encontró un CircleCollider2D en la burbuja.");
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
        spriteRenderer.color = Color.cyan;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Paredes"), true);
    }

    public void DesactivateFreezePowerUp()
    {
        frozen = false;
        spriteRenderer.color = originalColor;
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
        }
        else
        {
            //GameOVer
            Destroy(gameObject);
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
                Debug.LogWarning("El índice de 'colliderSizes' está fuera de rango o no está configurado.");
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
