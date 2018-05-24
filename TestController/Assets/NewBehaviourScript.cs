﻿using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

    public float speed = 10f;
    public float runSpeed = 5f;
    public float turnSmoothing = 15f;
    public float jumpForce = 100f;

    private Vector3 movement;
    private Rigidbody playerRigidBody;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");

        Move(lh, lv);
    }


    void Move(float lh, float lv)
    {
        movement.Set(lh, 0f, lv);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = movement.normalized * runSpeed * Time.deltaTime;
        }
        else
        {
            movement = movement.normalized * speed * Time.deltaTime;
        }

        playerRigidBody.MovePosition(transform.position + movement);


        if (lh != 0f || lv != 0f)
        {
            Rotating(lh, lv);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce);
        }
    }


    void Rotating(float lh, float lv)
    {
        Vector3 targetDirection = new Vector3(lh, 0f, lv);

        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);

        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }
}