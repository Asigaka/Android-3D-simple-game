using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;
    //[SerializeField] private LayerMask interactiveLayer;
    [SerializeField] private float range = 3;

    private Interactive interactive;
    private Interactive lastInteractive;
    private PlayerInteractionUI interactionUI;

    public static PlayerInteraction Instance;

    public Interactive Interactive { get => interactive; private set => interactive = value; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }

    private void Start()
    {
        interactionUI = PlayerInteractionUI.Instance;
    }

    private void Update()
    {
        InteractiveRay();
    }

    private void InteractiveRay()
    {
        interactive = null;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            interactive = hit.transform.GetComponent<Interactive>();
        }

        if (interactive != null)
        {
            if (interactive.IsLocked)
            {
                interactive = null;
                return;
            }

            if (interactive != lastInteractive)
            {
                interactive.OnFocused();
                lastInteractive = interactive;
            }

            interactionUI.TurnOnInteractiveCrosshair(interactive.Name);
        }
        else if (lastInteractive != null)
        {
            lastInteractive.OnDisfocused();
            lastInteractive = null;
        }
        else
        {
            interactionUI.TurnOffInteractiveCrosshair();
        }
    }

    public void OnInteractive()
    {
        if (interactive != null)
        {
            interactive.OnInteractive();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range);
    }
}
