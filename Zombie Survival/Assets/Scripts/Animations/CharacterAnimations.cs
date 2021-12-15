using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private string xStr = "x";
    [SerializeField] private string zStr = "z";
    [SerializeField] private string isRunningStr = "IsRunning";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWithoutGunMove(float x, float z)
    {
        animator.SetFloat(xStr, x);   
        animator.SetFloat(zStr, z);   
    }

    public void SetBoolRunning(bool isRunning)
    {
        animator.SetBool(isRunningStr, isRunning);
    }
}
