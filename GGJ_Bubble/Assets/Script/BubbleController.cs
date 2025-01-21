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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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
            Destroy(gameObject);
        }
    }

}
