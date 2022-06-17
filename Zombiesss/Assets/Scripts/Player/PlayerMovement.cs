using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick lookJoystick;
    [SerializeField] private Rigidbody rb;

    [Space]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotationSpeed;

    private Vector3 moveVelocity;
    private Vector3 lookVelocity;

    [HideInInspector] public UnityEvent onShotInput = new UnityEvent();

    private void Update()
    {
#if UNITY_EDITOR
        moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
#else
        moveVelocity = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);
#endif
        //moveVelocity = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);
        lookVelocity = new Vector3(lookJoystick.Horizontal, 0, lookJoystick.Vertical);

        if (lookVelocity.magnitude > 0.1f)
        {
            Look();
        }

        if (lookVelocity.magnitude > 0.7f)
        {
            onShotInput.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (moveVelocity.magnitude > 0.1f)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 move = transform.right * moveVelocity.x + transform.forward * moveVelocity.z;

        rb.MovePosition(transform.position + (move * moveSpeed * Time.deltaTime));
    }

    private void Look()
    {
        float heading = Mathf.Atan2(lookVelocity.x, lookVelocity.z);
        transform.rotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);
    }

    private void LookToMove()
    {
        float heading = Mathf.Atan2(moveVelocity.x, moveVelocity.z);
        transform.rotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);
    }
}
