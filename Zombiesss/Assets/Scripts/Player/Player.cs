using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerCombat combat;

    public PlayerMovement Movement { get => movement; }
    public PlayerCombat Combat { get => combat; }

    private void Start()
    {
        movement.onShotInput.AddListener(combat.TryShot);
    }
}
