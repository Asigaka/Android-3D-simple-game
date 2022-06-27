using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float radius;
    [Range(0, 360)]
    [SerializeField] private float angle;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask defaulLayer;

    public Health Targethealth  { get; private set; }
    public bool IsSeeHuman { get; private set; }
    public float Radius { get => radius;}
    public float Angle { get => angle;}

    public void CheckHumanAround()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetLayer);

        if (rangeChecks.Length != 0)
        {
            foreach (Collider collider in rangeChecks)
            {
                Health health = collider.GetComponent<Health>();

                if (health == null) return;

                Vector3 directionToTarget = (health.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, health.transform.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, defaulLayer))
                    {
                        IsSeeHuman = true;
                        Targethealth = health;
                        return;
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
        }
        else if (IsSeeHuman)
            IsSeeHuman = false;
    }
}
