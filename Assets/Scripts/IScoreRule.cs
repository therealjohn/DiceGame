using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreRule
{
    int GetScore(Dice[] dice, int bet);
}
