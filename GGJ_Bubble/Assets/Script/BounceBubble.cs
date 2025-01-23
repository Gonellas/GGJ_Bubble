using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBubble : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 0.5f;
    private void OnCollisionEnter2D(Collision2D collision)
     {

         if (collision.gameObject.CompareTag("Player"))
         {
             Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

             if (playerRb != null)
             {
                 ContactPoint2D contact = collision.GetContact(0);
                 Vector2 normal = contact.normal;
                 Vector2 bounceDirection = Vector2.Reflect(playerRb.velocity.normalized, normal);
                 playerRb.velocity = Vector2.zero;
                 playerRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
             }
         }   
     }
   /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que la burbuja principal tiene este tag.
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRigidbody != null)
            {
                // Obtener la normal del punto de colisión
                Vector2 collisionNormal = collision.contacts[0].normal;

                // Obtener la velocidad de la burbuja principal antes del impacto
                Vector2 incomingVelocity = playerRigidbody.velocity;

                // Calcular la nueva dirección de rebote usando la normal
                Vector2 bounceDirection = Vector2.Reflect(incomingVelocity.normalized, collisionNormal);
                // Ajustar si el ángulo es muy extremo o perpendicular
                float dotProduct = Vector2.Dot(incomingVelocity.normalized, collisionNormal);
                if (Mathf.Abs(dotProduct) > 0.80f) // Casi perpendicular
                {
                    bounceDirection = -collisionNormal; // Rebota directamente en la dirección opuesta
                }
                // Ajustar la velocidad de rebote (puedes modificar "bounceFactor")
                float bounceFactor = 20f; // Cambia este valor según necesites
                Vector2 reboundVelocity = bounceDirection * incomingVelocity.magnitude * bounceFactor;

                // Aplicar la nueva velocidad a la burbuja principal
                playerRigidbody.velocity = reboundVelocity;

           
            }
        }
    }*/
}

