using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController myController;

    public GameObject myCamera;

    public float walkSpeed;
    public float sprintSpeed;
    private bool isSprinting = false;
    public float gravity = -22f;
    public float jumpHeight;

    Vector3 velocity;

    private bool isCrouching = false;
    private bool isWalking = false;
    public float crouchSpeed;
    Vector3 crouch;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    bool isGrounded = true;

    public bool gamePaused;

    public bool canCrouch = true, canSprint = true, canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<CharacterController>();

    }


    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            Gravity();
            MyInput();
            if (Input.GetButtonDown("Jump") && isGrounded && canJump)
            {
                Jump();
            }

            //Check if Sprinting
            if (Input.GetKeyDown(KeyCode.LeftShift) && canSprint)
                isSprinting = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift) && canSprint)
                isSprinting = false;

            //Check if Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl) && canCrouch)
            {
                isCrouching = true;
                Crouch();
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl) && canCrouch)
            {
                isCrouching = false;
                Crouch();
            }
        }
    }

    private void Crouch()
    {
        Vector3 currentScale = transform.localScale;
        if (isCrouching)
        {
            currentScale.y /= 2;
        }
        else if (!isCrouching)
        {
            currentScale.y *= 2;
        }
        transform.localScale = currentScale;
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;

        myController.Move(velocity * Time.deltaTime);
    }

    private void MyInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude == 0f)
        {
            isWalking = false;
        }

        if (isSprinting && !isCrouching)
        {
            myController.Move(move * sprintSpeed * Time.deltaTime);
        }
        else if (!isSprinting && !isCrouching)
        {
            myController.Move(move * walkSpeed * Time.deltaTime);
            if (move.magnitude > 0f)
                isWalking = true;
        }
        else if (isCrouching)
        {
            myController.Move(move * crouchSpeed * Time.deltaTime);
        }
    }

}
