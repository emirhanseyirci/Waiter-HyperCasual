using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public Rigidbody rb;
    public float PlayerSpeed;
    public float forwardSpeed;
    public ButtonsController buttonsController;
    public float balance;
    public Vector3 rotationAngles;
    public float maxRotationZAngle;
    public float currentXPosition;
    public float rotationLerpSpeed;
    public float balanceChangeValue;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rotationLerpSpeed = 1f;
        //forwardSpeed = 2f;
        maxRotationZAngle = -35f;
       // PlayerSpeed = 3f;
        balanceChangeValue = 0.01f;
    }

    void Update()
    {
        currentXPosition = transform.position.x;
        Vector3 moveDirection = new Vector3(0, 0, forwardSpeed);
        transform.Translate(moveDirection * forwardSpeed * Time.deltaTime);
        UpdateRotation();
        transform.localEulerAngles = rotationAngles;
        forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, 16f);
    }

    public void LeftInclination()
    {
        balance = balance - balanceChangeValue;
    }


    public void RightInclination()
        {
            balance = balance + balanceChangeValue;
        }

        public void ResetBalanceValue()
        {
            balance = 0f;
        }


        public void IncreaseSpeedValue()
        {
            forwardSpeed += 0.4f;
            maxRotationZAngle -= 1f;
        }

        public void DecreaseSpeedValue()
        {
            forwardSpeed -= 1f;
        }

        public void StartGame()
        {
            forwardSpeed = 3.5f;
        }

        public void UpdateRotation()
        {
            float targetrot = buttonsController.OrientationAdjustmentValue * 30f;
            rotationAngles.y = Mathf.Lerp(rotationAngles.y, targetrot, rotationLerpSpeed * Time.deltaTime);
            rotationAngles.z = Mathf.Lerp(rotationAngles.z, buttonsController.OrientationAdjustmentValue * maxRotationZAngle, rotationLerpSpeed * Time.deltaTime);
        }


  }

    
