using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BounceBubble2 : MonoBehaviour
    {
        [SerializeField]
      private float bounceForce = 0.5f;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (playerRb != null)
                {
                    Vector2 bounceDirection = (collision.transform.position - transform.position).normalized;
                    playerRb.velocity = Vector2.zero; 
                    playerRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
                }
            }
        }
}
