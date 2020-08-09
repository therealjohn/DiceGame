using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        Debug.Log("Player turn finished. Resetting.");

        GameManager.IsPlayerRolling = false;
        GameManager.SetState(new GameStartState(GameManager));
    }

    private void CheckWinConditions(Dice[] dice, int bet)
    {
        var diceRollValues = dice.Select(x => x.sideValue).ToArray();
        var result = new ScoreRuleValidator().Validate(diceRollValues);

        Debug.Log($"Roll result\nFault: {result.IsFault}\nScore: {result.Score}\nMultiplier: {result.Multiplier}");
    }
}
