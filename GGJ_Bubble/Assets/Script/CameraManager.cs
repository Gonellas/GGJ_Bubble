using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform bubbleTransform;
    public Transform cameraTransform;
    public Rigidbody2D bubbleRigidbody;
    public float rotatioSpeed = 50f;
    public float returnSpeed = 100f;

    public bool isReturningToOrigin = false;

    public float rotationStep = 90f;
    private bool isRotatingToAngle = false;
    private Quaternion targetRotation;
    public float rotationSpeedStep = 250f;



    private void FixedUpdate()
    {
        if (bubbleTransform != null && bubbleRigidbody != null)
        {
            bubbleRigidbody.MoveRotation(cameraTransform.eulerAngles.z);
        }
    }

    void Update()
    {
        if (!isReturningToOrigin && !isRotatingToAngle)
        {
            RotateCamera();
            CheckQuickRotationInput(); 
        }
        else if (isReturningToOrigin)
        {
            ReturnToOrigin();
        }
        else if (isRotatingToAngle)
        {
            RotateToTargetAngle();
        }
    }
    //
    private void CheckQuickRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartQuickRotation(-rotationStep); // Hacia la derecha
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartQuickRotation(rotationStep); // Hacia la izquierda
        }
    }

    private void StartQuickRotation(float angle)
    {
        isRotatingToAngle = true;
        targetRotation = Quaternion.Euler(0, 0, cameraTransform.eulerAngles.z + angle);
    }

    private void RotateToTargetAngle()
    {
        if (cameraTransform != null)
        {
            cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, targetRotation, rotationSpeedStep * Time.deltaTime);

            if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 0.1f)
            {
                cameraTransform.rotation = targetRotation;
                isRotatingToAngle = false;
            }
        }
    }
    //

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
           /* if (bubbleTransform != null) 
            {
                bubbleTransform.rotation = cameraTransform.rotation;
            }*/
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

               /* if (bubbleTransform != null)
                {
                    bubbleTransform.rotation = targetRotation;
                }*/
            }
        }
    }

    public void TriggerReturnToOrigin()
    {
        isReturningToOrigin = true;
    }


}
