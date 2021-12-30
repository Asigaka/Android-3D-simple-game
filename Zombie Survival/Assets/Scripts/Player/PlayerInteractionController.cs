using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;
    //[SerializeField] private LayerMask interactiveLayer;
    [SerializeField] private float range = 3;

    private Interactive interactive;

    public Interactive lastInteractive;

    public static PlayerInteractionController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
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
            if (interactive != lastInteractive)
            {
                interactive.OnFocused();
                lastInteractive = interactive;
            }
        }
        else if (lastInteractive != null)
        {
            lastInteractive.OnDisfocused();
            lastInteractive = null;
        }
    }

    public void Interactive()
    {
        interactive.OnInteractive();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range);
    }
}
