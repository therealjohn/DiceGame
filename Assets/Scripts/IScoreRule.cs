using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreRule
{
    ScoreResult GetScoreResult(int[] diceRollValues);
}
