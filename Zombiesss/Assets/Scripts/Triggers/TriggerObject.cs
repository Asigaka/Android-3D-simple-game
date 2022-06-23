using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerObject : MonoBehaviour
{
    [SerializeField] private bool isPlayerTrigger = true;

    public UnityEvent onTrigger = new UnityEvent();

    private Collider trigger;

    private void Awake()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;
        GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerTrigger)
        {
            Player player = other.GetComponent<Player>();

            if (player)
            {
                onTrigger.Invoke();
                Destroy(gameObject);
            }
        }
        else
        {
            onTrigger.Invoke();
            Destroy(gameObject);
        }
    }
}
