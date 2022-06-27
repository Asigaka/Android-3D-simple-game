using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerNeeds))]
public class PlayerNeedsController : MonoBehaviour
{
    private PlayerNeeds needs;

    public static PlayerNeedsController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }

    private void Start()
    {
        needs = GetComponent<PlayerNeeds>();
        SetStartNeedsAmount();
    }

    private void SetStartNeedsAmount()
    {
        needs.Satiety.CurrentAmount = needs.Satiety.MaxAmount;
        needs.Thirst.CurrentAmount = needs.Thirst.MaxAmount;
    }

    public void ChangeNeedsPerMinute()
    {
        ChangeNeedPerMinute(needs.Satiety);
        ChangeNeedPerMinute(needs.Thirst);
        PlayerNeedsUI.Instance.ShowAllNeeds(needs.Health.CurrentAmount,
            needs.Satiety.CurrentAmount, needs.Thirst.CurrentAmount);
    }

    public void ChangeNeedAmount(PlayerNeeds.NeedType type, float amount)
    {
        switch (type)
        {
            case PlayerNeeds.NeedType.Health: needs.Health.CurrentAmount += amount; break;
            case PlayerNeeds.NeedType.Satiety: needs.Satiety.CurrentAmount += amount; break;
            case PlayerNeeds.NeedType.Thirst: needs.Thirst.CurrentAmount += amount; break;
        }
    }

    private void ChangeNeedPerMinute(PlayerNeeds.Need need)
    {
        need.CurrentAmount += need.ChangePerMinute;

        if (need.CurrentAmount > need.MaxAmount)
            need.CurrentAmount = need.MaxAmount;
        else if (need.CurrentAmount < 0)
            need.CurrentAmount = 0;
    }
}
