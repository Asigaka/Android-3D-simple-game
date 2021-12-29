using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float radius;
    [Range(0, 360)]
    [SerializeField] private float angle;

    [SerializeField] private LayerMask humanLayer;
    [SerializeField] private LayerMask defaulLayer;

    public Vector3 HumanPos { get; private set; }
    public bool IsSeeHuman { get; private set; }
    public float Radius { get => radius;}
    public float Angle { get => angle;}

    public void CheckHumanAround()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, humanLayer);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, defaulLayer))
                {
                    HumanPos = target.position;
                    IsSeeHuman = true;
                }
                else
                {
                    IsSeeHuman = false;
                }
            }
            else
            {
                IsSeeHuman = false;
            }
        }
        else if (IsSeeHuman)
            IsSeeHuman = false;
    }
}
