using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    private Vector3 noisePos;
    private float range;

    public void GenerateNoise(float range, Vector3 noisePos)
    {
        this.range = range;
        this.noisePos = noisePos;
        Destroy(this, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(noisePos, range);
    }
}
