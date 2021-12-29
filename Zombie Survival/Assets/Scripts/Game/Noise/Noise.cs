using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    private Vector3 noisePos;
    private float range;

    public void GenerateNoise(Vector3 noisePos, float range)
    {
        this.range = range;
        this.noisePos = noisePos;
        Collider[] checkColliders = Physics.OverlapSphere(noisePos, range);
        foreach (Collider collider in checkColliders)
        {
            if (collider.GetComponent<ICreature>() != null)
            {
                collider.GetComponent<ICreature>().OnHeardNoise(noisePos);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(noisePos, range);
    }
}
