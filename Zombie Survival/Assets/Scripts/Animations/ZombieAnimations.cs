using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieAnimations : MonoBehaviour
{
    private const string walk = "walk";
    private const string scream = "scream";
    private const string attack_1 = "attack_1";
    private const string attack_2 = "attack_2";
    private const string attack_3 = "attack_3";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRandomAttackAnim()
    {
        int num = Random.Range(1, 3);
        animator.SetTrigger("attack_" + num);
    }

    public void SetWalk(bool state)
    {
        animator.SetBool(walk, state);
    }
}
