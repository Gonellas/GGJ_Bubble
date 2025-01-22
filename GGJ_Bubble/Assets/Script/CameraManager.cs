using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform cameraTransform;
    public float rotatioSpeed = 50f;
    public float returnSpeed = 100f;

    public bool isReturningToOrigin = false;


    void Start()
    {
        
    }

    void Update()
    {
        if (!isReturningToOrigin)
        {
            RotateCamera();
        }
        else
        {
            ReturnToOrigin();
        }
    }

    private void RotateCamera()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (cameraTransform != null)
        {
            cameraTransform.Rotate(Vector3.forward, horizontalInput * rotatioSpeed * Time.deltaTime);

            float zRotation = cameraTransform.eulerAngles.z;

            if (zRotation > 359.99f || zRotation < -359.99f ) 
            {
                cameraTransform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void ReturnToOrigin()
    {
        if (cameraTransform != null)
        {
            Quaternion currentRotation = cameraTransform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

            cameraTransform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, returnSpeed * Time.deltaTime);

            if (Quaternion.Angle(currentRotation, targetRotation) < 0.1f)
            {
                //isReturningToOrigin = false;
                cameraTransform.rotation = targetRotation;
            }
        }
    }

    public void TriggerReturnToOrigin()
    {
        isReturningToOrigin = true;
    }


}
