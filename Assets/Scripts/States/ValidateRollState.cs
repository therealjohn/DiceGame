﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateRollState : GameState
{
    Dice[] dice;
    bool needsReroll;

    public ValidateRollState(GameManager gameManager) 
        : base(gameManager)
    {
    }

    public override void Start()
    {
        dice = GameManager.Dice;
    }

    public override void Update()
    {
        if(needsReroll)
        {
            GameManager.SetState(new PlayerTurnState(GameManager));
        }
        else if(AllDiceDoneRolling())
        {
            GameManager.SetState(new ScoringState(GameManager));
        }
    }

    private bool AllDiceDoneRolling()
    {
        foreach (Dice d in dice)
        {
            if(d.IsDoneRolling)
            {
                needsReroll = CheckIfNeedsRerolled(d);
                d.canRoll = needsReroll;
            }

            if (!d.IsDoneRolling)
                return false;
        }

        return true;
    }

    private bool CheckIfNeedsRerolled(Dice d)
    {
        // The sideValue will be -1 if a proper collision did not occur
        // to calculate the sideValue
        return d.sideValue <= 0;
    }
}
