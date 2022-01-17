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
        Invoke("ClearNoise", 1);
    }

    private void ClearNoise()
    {
        range = 0;
        noisePos = new Vector3();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(noisePos, range);
    }
}
