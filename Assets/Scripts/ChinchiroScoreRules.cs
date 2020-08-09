using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AtleastTwoDiceMatchWinBet : IScoreRule
{   
    public ScoreResult GetScoreResult(int[] diceRollValues)
    {
        var result = new ScoreResult
        {
            IsFault = true
        };

        for (int i = 1; i <= 6; i++)
        {
            if (diceRollValues.Count(x => x == i) >= 2)
            {
                result.Score = i;
                result.Multiplier = 1;
                result.IsFault = false;
                break;
            }
        }

        return result;
    }
}

public class TripleMatchWinTripleBet : IScoreRule
{   
    public ScoreResult GetScoreResult(int[] diceRollValues)
    {
        var result = new ScoreResult();

        for (int i = 1; i <= 6; i++)
        {
            if (diceRollValues.Count(x => x == i) == 3)
            {
                result.Multiplier = 3;
                break;
            }
        }

        return result;
    }
}

public class TripleOneLoseTripleBet : IScoreRule
{
    int[] win = new int[3] { 1, 1, 1 };

    public ScoreResult GetScoreResult(int[] diceRollValues)
    {
        var result = new ScoreResult();

        if (diceRollValues.All(x => win.Contains(x)))
            result.Multiplier = -3;

        return result;
    }
}

public class OneTwoThreeLoseDoubleBet : IScoreRule
{
    int[] win = new int[3] { 1, 2, 3 };

    public ScoreResult GetScoreResult(int[] diceRollValues)
    {
        var result = new ScoreResult();

        if (diceRollValues.All(x => win.Contains(x)))
            result.Multiplier = -2;

        return result;
    }
}

public class FourFiveSixWinDoubleBet : IScoreRule
{
    int[] win = new int[3] { 4, 5, 6 };

    public ScoreResult GetScoreResult(int[] diceRollValues)
    {
        var result = new ScoreResult();

        if (diceRollValues.All(x => win.Contains(x)))
            result.Multiplier = 2;

        return result;
    }
}
