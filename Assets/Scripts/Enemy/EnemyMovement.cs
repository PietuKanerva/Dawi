using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public CharacterController enemy_controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheckd;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;


    void Update()
    {
        Movement();
    }


    #region Movement
    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheckd.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        Vector3 move = transform.forward * Time.deltaTime;

        enemy_controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        enemy_controller.Move(velocity * Time.deltaTime);
    }
    #endregion 
}
