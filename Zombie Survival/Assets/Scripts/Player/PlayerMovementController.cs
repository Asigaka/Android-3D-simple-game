using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private CharacterAnimations animations;

    [Space(7)]
    [SerializeField] private float walkSpeed = 6;
    [SerializeField] private float runSpeed = 12;

    [Space(7)]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 12;

    private CharacterController controller;

    private enum State { IDLE, WALK, RUN, JUMP, FALL}
    private State state = State.IDLE;
    private State lastState;

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        //animations.SetWithoutGunMove(x, z);

        switch (state)
        {
            case State.IDLE:
                if (x != 0 || z != 0)
                    SetState(State.WALK);
                else if ((x != 0 || z != 0 && Input.GetKey(KeyCode.LeftShift)))
                     SetState(State.RUN);

                if (Input.GetButtonDown("Jump"))
                    SetState(State.JUMP);

                break;
            case State.WALK:
                Walk(x, z);
                if (x == 0 && z == 0)
                    SetState(State.IDLE);

                if (Input.GetButtonDown("Jump"))
                    SetState(State.JUMP);

                if ((x != 0 || z != 0 && Input.GetKey(KeyCode.LeftShift)))
                    SetState(State.RUN);

                break;
            case State.RUN:
                Run(x, z);
                if (x == 0 && z == 0)
                    SetState(State.IDLE);

                if (!Input.GetKey(KeyCode.LeftShift))
                    SetState(State.WALK);

                if (Input.GetButtonDown("Jump"))
                    SetState(State.JUMP);

                break;
            case State.JUMP:
                Jump();
                if (!isGrounded)
                    SetState(State.FALL);

                break;
            case State.FALL:
                if (isGrounded)
                    SetState(State.IDLE);

                break;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void Walk(float x, float z)
    {
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * walkSpeed * Time.deltaTime);
        //animations.SetBoolRunning(false);
    }

    private void Run(float x, float z)
    {
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * runSpeed * Time.deltaTime);
        //animations.SetBoolRunning(true);
    }

    private void SetState(State to)
    {
        lastState = state;
        state = to;
    }
}
