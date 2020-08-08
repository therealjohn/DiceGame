using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AtleastTwoDiceMatchWinBet : IScoreRule
{   
    public int GetScore(Dice[] dice, int bet)
    {
        int score = bet;

        for (int i = 1; i <= 6; i++)
        {
            if (dice.Count(x => x.sideValue == i) >= 2)
            {
                score = i;
                break;
            }
        }

        return score;
    }
}

public class TripleMatchWinTripleBet : IScoreRule
{   
    public int GetScore(Dice[] dice, int bet)
    {
        int score = bet;

        for (int i = 1; i <= 6; i++)
        {
            if (dice.Count(x => x.sideValue == i) == 3)
            {
                score = i;
                break;
            }
        }

        return score;
    }
}

public class TripleOneLoseTripleBet : IScoreRule
{
    int[] win = new int[3] { 1, 1, 1 };

    public int GetScore(Dice[] dice, int bet)
    {
        int[] values = dice.Select(x => x.sideValue).ToArray();

        if (values.All(x => win.Contains(x)))
            return -bet * 3;

        return bet;
    }
}

public class OneTwoThreeLoseDoubleBet : IScoreRule
{
    int[] win = new int[3] { 1, 2, 3 };

    public int GetScore(Dice[] dice, int bet)
    {
        int[] values = dice.Select(x => x.sideValue).ToArray();

        if (values.All(x => win.Contains(x)))
            return -bet * 2;

        return bet;
    }
}

public class FourFiveSixWinDoubleBet : IScoreRule
{
    int[] win = new int[3] { 4, 5, 6 };

    public int GetScore(Dice[] dice, int bet)
    {
        int[] values = dice.Select(x => x.sideValue).ToArray();

        if (values.All(x => win.Contains(x)))
            return bet * 2;

        return bet;
    }
}
