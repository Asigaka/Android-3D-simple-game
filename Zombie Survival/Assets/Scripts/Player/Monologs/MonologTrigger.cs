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
            Session.Instance.Monologs.AddMonolog(info);
            Destroy(gameObject);
        }    
    }
}
