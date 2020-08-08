using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : StateManager
{
    [SerializeField] private Player player;
    [SerializeField] private ChinchiroGame gameSystem;

    [Header("Opponents")]
    [SerializeField] private Opponent[] opponents;

    [Header("Game")]
    [SerializeField] private Dice[] dice;    

    [Header("UI")]
    [SerializeField] private Text scoreText;

    private int currentFaults, currentOpponentIndex;
    private Opponent currentOpponent;
    private int playerBetAmount = 100;

    public Dice[] Dice => dice;
    public int PlayerBetAmount => playerBetAmount;
    public bool IsPlayerRolling { get; set; }

    private void Start()
    {
        SetState(new GameStartState(this));
    }

    private void Update()
    {
        state?.Update();
    }

    public void RollDice()
    {
        foreach (Dice d in Dice)
        {
            if (d.canRoll)
            {
                d.Roll();
            }
        }
    }

    internal void ResetGame()
    {
        foreach (Dice d in dice)
        {
            d.Reset();
        }
    }

    //private GameState UpdateIdle(bool playerRequestedRoll)
    //{        
    //    if(GameRuleMaxFaults())
    //    {
    //        return ChangeGameState(GameState.RoundLost);
    //    }

    //    if (playerRequestedRoll && turn == Turn.player)
    //    {
    //        ResetScoreText();
    //        RollDice();

    //        return GameState.Rolling;
    //    }

    //    return GameState.Idle;
    //}

    //private GameState ChangeGameState(GameState gameState)
    //{
    //    Debug.Log($"[{DateTime.Now.ToLongTimeString()}] Changed to state {gameState}");
    //    return gameState;
    //}

    //private bool GameRuleMaxFaults() => currentFaults >= 3;

    //private GameState UpdateRolling()
    //{
    //    if(AllDiceDoneRolling())
    //    {
    //        return GameState.CheckResults;
    //    }

    //    return GameState.Rolling;
    //}    

    //private GameState UpdateCheckResults()
    //{
    //    if (needToReroll)
    //    {
    //        UpdateScoreText("Keep trying");
    //        return GameState.Idle;
    //    }

    //    int score = GetScore();

    //    if (score <= 0)
    //    {            
    //        currentFaults++;
    //        UpdateScoreText("Fault! Roll again.");
    //        Debug.Log($"Fault! (Total faults: {currentFaults})");
    //    }
    //    else
    //    {
    //        currentFaults = 0;
    //        UpdateScoreText(score);
    //    }

    //    if(currentFaults >= 3)
    //    {
    //        return GameState.RoundLost;
    //    }

    //    StartNewTurn();
    //    return GameState.Idle;
    //}

    //private GameState UpdateLose()
    //{
    //    UpdateScoreText("3 faults, you lose.");

    //    // Reset faults for a new round
    //    currentFaults = 0;

    //    StartNewTurn();

    //    return GameState.Idle;
    //}



    //private int GetScore()
    //{
    //    int score = 0;
    //    var values = new List<int>();

    //    foreach (Dice d in dice)
    //        values.Add(d.sideValue);

    //    for(int i = 1; i <= 6; i++)
    //    {
    //        if(values.Count(x => x == i) >= 2)
    //        {
    //            score = i;
    //            Debug.Log($"Score: {score}");
    //            break;
    //        }
    //    }

    //    return score;
    //}

    public void ResetScoreText()
    {
        scoreText.text = string.Empty;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = $"You rolled {score}";
    }

    public void UpdateScoreText(string text)
    {
        scoreText.text = text;
    }
}
