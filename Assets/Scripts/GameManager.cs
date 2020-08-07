using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Idle, Rolling, CheckResults, Lose, Win }

public class GameManager : MonoBehaviour
{
    [SerializeField] private Dice[] dice;
    [SerializeField] private Text scoreText;

    public GameState state;

    private int currentFaults = 0;

    private void Start()
    {
        state = GameState.Idle;
        ResetScoreText();
    }

    public void RollDice()
    {
        foreach(Dice d in dice)
        {
            if (d.canRoll)
            {
                d.Roll();
            }
        }
    }

    private void Update()
    {
        if (state == GameState.Idle)
            state = UpdateIdle();
        else if (state == GameState.Rolling)
            state = UpdateRolling();
        else if (state == GameState.CheckResults)
            state = UpdateCheckResults();
        else if (state == GameState.Lose)
            state = UpdateLose();
    }

    private GameState UpdateIdle()
    {
        if(currentFaults >= 3)
        {
            return GameState.Lose;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetScoreText();
            RollDice();

            return GameState.Rolling;
        }

        return GameState.Idle;
    }

    private GameState UpdateRolling()
    {
        if(AllDiceDoneRolling())
        {
            return GameState.CheckResults;
        }

        return GameState.Rolling;
    }    

    private GameState UpdateCheckResults()
    {
        bool needToReroll = false;

        foreach (Dice d in dice)
        {
            // Check if any dice need rerolled
            // (landed on an edge / corner / etc.)
            d.canRoll = CheckIfNeedsRerolled(d);

            if (d.canRoll)
                needToReroll = true;
        }

        if (needToReroll)
        {
            UpdateScoreText("Keep trying");
            return GameState.Idle;
        }

        int score = GetScore();

        if (score <= 0)
        {            
            currentFaults++;
            UpdateScoreText("Fault! Roll again.");
            Debug.Log($"Fault! (Total faults: {currentFaults})");
        }
        else
        {
            currentFaults = 0;
            UpdateScoreText(score);
        }

        if(currentFaults >= 3)
        {
            return GameState.Lose;
        }

        StartNewTurn();
        return GameState.Idle;
    }

    private GameState UpdateLose()
    {
        UpdateScoreText("3 faults, you lose.");

        // Reset faults for a new round
        currentFaults = 0;

        StartNewTurn();

        return GameState.Idle;
    }

    private bool CheckIfNeedsRerolled(Dice d)
    {
        // The sideValue will be -1 if a proper collision did not occur
        // to calculate the sideValue
        return d.sideValue <= 0;
    }

    private bool AllDiceDoneRolling()
    {
        foreach(Dice d in dice)
        {
            if (!d.IsDoneRolling)
                return false;
        }

        return true;
    }

    private int GetScore()
    {
        int score = 0;
        var values = new List<int>();

        foreach (Dice d in dice)
            values.Add(d.sideValue);

        for(int i = 1; i <= 6; i++)
        {
            if(values.Count(x => x == i) >= 2)
            {
                score = i;
                Debug.Log($"Score: {score}");
                break;
            }
        }

        return score;
    }

    private void StartNewTurn()
    {
        foreach (Dice d in dice)
        {
            d.Reset();
        }
    }

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
