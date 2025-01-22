using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBubble : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 5f; // Ajusta la fuerza según tu necesidad
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Obtén la normal del punto de contacto
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 normal = contact.normal;

                // Calcula la dirección del rebote (opuesta a la normal)
                Vector2 bounceDirection = normal;

                // Limpia la velocidad actual del jugador para evitar acumulaciones
                playerRb.velocity = Vector2.zero;

                // Aplica la fuerza de rebote en la dirección calculada
                playerRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}

