using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologsController : MonoBehaviour
{
    [SerializeField] private List<MonologEntity> monologsQueue;

    [SerializeField] private float localMonologsTimer = 0;

    private void Update()
    {
        if (CanShowMonologByTimer() && monologsQueue.Count > 0)
        {
            ShowMonologs();
        }
    }

    private void ShowMonologs()
    {
        //MonologsUI.Instance.ShowMonolog(monologsQueue[0].MonologText);
        monologsQueue.RemoveAt(0);
    }

    private bool CanShowMonologByTimer()
    {
        if (localMonologsTimer <= 0)
        {
            if (monologsQueue.Count > 0)
            {
                localMonologsTimer = monologsQueue[0].MonologTime;
            }
            else
            {
                //MonologsUI.Instance.ClearMonologText();
                localMonologsTimer = 0;
            }
              
            return true;
        }
        else
        {
            localMonologsTimer -= Time.deltaTime;
            return false;
        }
    }

    public void AddMonolog(MonologInfo monolog)
    {
        monologsQueue = monolog.monologEntities.GetRange(0, monolog.monologEntities.Count);
    }
}
