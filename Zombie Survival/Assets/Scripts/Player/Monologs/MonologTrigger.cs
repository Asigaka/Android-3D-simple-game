using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    [SerializeField] private MonologInfo info;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonologsController.Instance.AddMonolog(info);
            Destroy(gameObject);
        }    
    }
}
