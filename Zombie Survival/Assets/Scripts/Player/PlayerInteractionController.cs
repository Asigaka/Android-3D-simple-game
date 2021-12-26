using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;
    //[SerializeField] private LayerMask interactiveLayer;
    [SerializeField] private float range = 3;

    public Interactive lastInteractive;

    private void Update()
    {
        InteractiveRay();
    }

    private void InteractiveRay()
    {
        Interactive interactive = null;
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

            if (Input.GetKeyDown(KeyCode.F))
                interactive.OnInteractive();
        }
        else if (lastInteractive != null)
        {
            lastInteractive.OnDisfocused();
            lastInteractive = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range);
    }
}
