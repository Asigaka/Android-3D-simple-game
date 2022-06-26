using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera actionCamera;
    [SerializeField] private Vector3 actionOffset;

    private void LateUpdate()
    {
        Vector3 targetPos = transform.position + actionOffset;
        actionCamera.transform.position = targetPos;
    }
}
