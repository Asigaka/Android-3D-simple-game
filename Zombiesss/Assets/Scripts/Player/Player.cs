using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private Health health;

    public PlayerMovement Movement { get => movement; }
    public PlayerCombat Combat { get => combat; }
    public Health Health { get => health; }

    private void Start()
    {
        movement.onShotInput.AddListener(combat.TryShot);
    }
}
