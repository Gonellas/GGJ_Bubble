using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Set Instance
    private static GameManager instance = null;

    //Gravity variables
    public Transform cameraTransform;

    //Bubble physics variables
    public float gravityStrength = 9.8f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        //Physics of bubble
        ChangeGravity(); 


    }
    private void ChangeGravity()
    {
        Vector2 gravityDirection = -cameraTransform.up;
        Physics2D.gravity = gravityDirection * gravityStrength;
    }

}
