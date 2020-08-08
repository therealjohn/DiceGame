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

    public ScoreResult Validate(int[] diceRollValues)
    {
        var result = new ScoreResult();

        foreach(var rule in scoreRules)
        {
            result = rule.Value.GetScoreResult(diceRollValues);

            if (result.IsFault || result.Multiplier > 0 || result.Score > 0)
                break;
        }

        return result;
    }

    private void Add(string ruleName, IScoreRule scoreRule)
    {
        scoreRules.Add(ruleName, scoreRule);
    }
}

public class ScoreResult
{
    public bool IsFault { get; set; }
    public int Multiplier { get; set; } = 0;
    public int Score { get; set; } = 0;
}
