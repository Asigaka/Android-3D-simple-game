using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature 
{
    public void OnHeardNoise(Vector3 noisePos);
    public void OnSeePlayer();
    public void OnMetObstacle();
}
