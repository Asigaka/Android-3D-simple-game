using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Joystick moveJoys;
    [SerializeField] private Joystick lookJoys;

    [Space(7)]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;

    [HideInInspector] public UnityEvent onShotInput = new UnityEvent();

    private CharacterController character;
    private float turnSmoothVelocity;
    private Vector3 lookDir;
    private Vector3 moveDir;
    private Vector3 velocity;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //GroundCheck();
        //PlayerGravity();

        Move();
    }

    private void Move()
    {
#if UNITY_EDITOR 
        float moveHor = Input.GetAxisRaw("Horizontal");
        float moveVer = Input.GetAxisRaw("Vertical");
#else
        float moveHor = moveJoys.Horizontal;
        float moveVer = moveJoys.Vertical;
#endif
        float lookHor = lookJoys.Horizontal;
        float lookVer = lookJoys.Vertical;

        moveDir = new Vector3(moveHor, 0, moveVer).normalized;
        lookDir = new Vector3(lookHor, 0, lookVer).normalized;

        if (isRunning() || isLooking())
        {
            float targetAngle;
            if (isLooking())
            {
                targetAngle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
                onShotInput.Invoke();
            }
            else
            {
                targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            }

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            character.Move(moveDir * 6 * Time.deltaTime);
        }
    }

    public void RotateToTarget(Transform target)
    {
        transform.LookAt(target);
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void PlayerGravity()
    {
        if (GroundCheck() && velocity.y < 0)
        {
            velocity.y = -2;
        }

        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
    }

    public bool isRunning()
    {
        return moveDir.magnitude >= 0.1f;
    }

    public bool isLooking()
    {
        return lookDir.magnitude >= 0.1f;
    }
}
