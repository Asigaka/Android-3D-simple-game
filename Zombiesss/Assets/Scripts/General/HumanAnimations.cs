using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private const string SPEED_KEY = "speed";
    private const string IS_FIRING_KEY = "isFire";

    public void SetSpeed(float speed) => anim.SetFloat(SPEED_KEY, speed);
    public void SetFiring(bool isFiring) => anim.SetBool(IS_FIRING_KEY, isFiring);
}
