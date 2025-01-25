using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Set Instance
    private static GameManager instance = null;

    public FinalCheck finalCheck;
    public CameraManager cameraManager;

    public Transform cameraTransform;
    public Transform bubbleTransform;
    public Rigidbody2D bubbleRigidBody;
    public Collider2D bubbleCollider;

    public Transform[] bubblePoint;
    public Transform[] cameraPoint;

    public float waitBeforeTransition = 1.5f;
    public float transitionDuration = 3f;


    private int currentLevel = 0;
    [SerializeField] private bool isTransitioning = false;

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
        if (bubbleRigidBody == null) bubbleRigidBody = GetComponent<Rigidbody2D>();
        if (bubbleCollider == null) bubbleCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (finalCheck.isBubbleInFinalCheck)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if (!isTransitioning && currentLevel < bubblePoint.Length -1)
        {
            StartCoroutine(TransitionToNextLevel());
        }
    }
    private IEnumerator TransitionToNextLevel()
    {
        isTransitioning = true;
        //Desactivamos fisicas de la burbuja
        bubbleRigidBody.velocity = Vector3.zero;
        bubbleRigidBody.isKinematic = true;
        bubbleCollider.enabled = false;

        //Enderezamos la camara asheeee.
        cameraManager.TriggerReturnToOrigin();


        //esperamos se enderece la camara
        yield return new WaitForSeconds(waitBeforeTransition);


        //Movemos la camara y la burbuja al siguiente nivel
        Transform nextCameraPoint = cameraPoint[currentLevel + 1];
        Vector3 cameraStartPos = cameraTransform.position;
        Vector3 cameraTargetPos = nextCameraPoint.position;

        Transform nextBubblePoint = bubblePoint[currentLevel + 1];
        Vector3 bubbleStartPos = bubbleTransform.position;
        Vector3 bubbleTargetPos = nextBubblePoint.position;

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            cameraTransform.position = Vector3.Lerp(cameraStartPos, cameraTargetPos, t);
            bubbleTransform.position = Vector3.Lerp(bubbleStartPos, bubbleTargetPos, t);

            yield return null;
        }
        //Aseguramos que la camara y bubuja este en la posicion objetivo
        cameraTransform.position = cameraTargetPos;
        bubbleTransform.position = bubbleTargetPos;

        //Activamos fisicas de la burbuja

        bubbleRigidBody.isKinematic = false;
        bubbleCollider.enabled = true;
        
        //Actulizar nivel actual
        currentLevel++;

        cameraManager.isReturningToOrigin = false;
        isTransitioning = false;
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
