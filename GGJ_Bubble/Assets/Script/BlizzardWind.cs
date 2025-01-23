using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardWind : MonoBehaviour
{
    public Vector2 windDirection = new Vector2(1, 0);
    public float windStrength = 5f;
    public Rigidbody2D bubbleRigidBody;
    private bool isInArea = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInArea = false;  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
            AudioManager.instance.PlaySFX(SoundType.WindSFX, 2.5f);
    }

    private void FixedUpdate()
    {
        if (isInArea)
        {
            bubbleRigidBody.AddForce(windDirection.normalized * windStrength);
        }
    }
}
