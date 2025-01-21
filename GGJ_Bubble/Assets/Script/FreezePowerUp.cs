using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerUp : MonoBehaviour
{
    public float duracion = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BubbleController bubbleController = collision.GetComponent<BubbleController>();
            if (bubbleController != null)
            {
                bubbleController.ActivateFreezePowerUp(duracion);
            }

            Destroy(gameObject); 
        }
    }
}
