using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRuleValidator
{
    private Dictionary<string, IScoreRule> scoreRules = new Dictionary<string, IScoreRule>();

    public ScoreRuleValidator()
    {
        Add(nameof(TripleOneLoseTripleBet), new TripleOneLoseTripleBet());
        Add(nameof(OneTwoThreeLoseDoubleBet), new OneTwoThreeLoseDoubleBet());
        Add(nameof(TripleMatchWinTripleBet), new TripleMatchWinTripleBet());
        Add(nameof(FourFiveSixWinDoubleBet), new FourFiveSixWinDoubleBet());
        Add(nameof(AtleastTwoDiceMatchWinBet), new AtleastTwoDiceMatchWinBet());
    }

    public int Validate(Dice[] dice, int bet)
    {
        int score = bet;

        foreach(var rule in scoreRules)
        {
            score = rule.Value.GetScore(dice, bet);

            if (score != bet)
                break;
        }

        return score;
    }

    private void Add(string ruleName, IScoreRule scoreRule)
    {
        scoreRules.Add(ruleName, scoreRule);
    }
}
