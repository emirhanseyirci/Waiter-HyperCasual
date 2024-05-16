using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMov : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // W tuşuna basıldığında yukarı yönde hareket
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // S tuşuna basıldığında aşağı yönde hareket
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // A tuşuna basıldığında sola doğru hareket
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
        }

        // D tuşuna basıldığında sağa doğru hareket
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
}
