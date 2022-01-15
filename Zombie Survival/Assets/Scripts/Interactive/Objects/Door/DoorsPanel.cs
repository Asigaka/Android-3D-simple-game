using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorsPanel : Interactive
{
    [SerializeField] private DoorController door;

    private void Start()
    {
        if (door == null)
        {
            Debug.Log("DoorController is null");
            return;
        }
    }

    public override void OnInteractive()
    {
        door.ToggleDoor();
    }
}
