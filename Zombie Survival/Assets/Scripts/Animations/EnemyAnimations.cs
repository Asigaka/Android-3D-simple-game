using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimations : MonoBehaviour
{
    private const string walk = "isWalking";
    private const string die = "die";
    private const string attack_1 = "attack_1";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackTrigger()
    {
        animator.SetTrigger(attack_1);
    }

    public void DieTrigger()
    {
        animator.SetTrigger(die);
    }

    public void SetWalk(bool state)
    {
        animator.SetBool(walk, state);
    }
}
