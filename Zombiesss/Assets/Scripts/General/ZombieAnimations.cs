using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private const string SPEED_KEY = "speed";
    private const string ATTACK_KEY = "attack";

    public void SetSpeed(float speed) => anim.SetFloat(SPEED_KEY, speed);
    public void SetAttack() => anim.SetTrigger(ATTACK_KEY);
}
