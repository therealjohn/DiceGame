using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringState : GameState
{
    int score = -1;
    int multiplier = 1;

    public ScoringState(GameManager stateManager) : base(stateManager)
    {
    }

    public override void Start()
    {
        var dice = GameManager.Dice;

        CheckWinConditions(dice, GameManager.PlayerBetAmount);

        GameManager.SetState(new GameStartState(GameManager));
    }

    private void CheckWinConditions(Dice[] dice, int bet)
    {
        int score = new ScoreRuleValidator().Validate(dice, bet);

        Debug.Log($"Rolled score {score}");
    }
}
