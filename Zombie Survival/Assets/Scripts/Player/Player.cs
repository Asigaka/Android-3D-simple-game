using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FirstPersonController movement;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerHands hands;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerInteraction interaction;
    [SerializeField] private Health health;

    public FirstPersonController Movement { get => movement; }
    public PlayerCombat Combat { get => combat; }
    public PlayerHands Hands { get => hands; }
    public PlayerInventory Inventory { get => inventory; }
    public Health Health { get => health; }
    public PlayerInteraction Interaction { get => interaction; }
}
