using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ScoreValidatorTests
    {
        [Test]
        public void triple_one_lose_triple_bet()
        {
            var rule = new TripleOneLoseTripleBet();
            var diceRollValues = new int[3] { 1, 1, 1 };

            var result = rule.GetScoreResult(diceRollValues);

            Assert.AreEqual(false, result.IsFault);
            Assert.AreEqual(-3, result.Multiplier);
            Assert.AreEqual(0, result.Score);
        }

        [Test]
        public void one_two_three_lose_double_bet()
        {
            var rule = new OneTwoThreeLoseDoubleBet();
            var diceRollValues = new int[3] { 1, 2, 3 };

            var result = rule.GetScoreResult(diceRollValues);

            Assert.AreEqual(false, result.IsFault);
            Assert.AreEqual(-2, result.Multiplier);
            Assert.AreEqual(0, result.Score);
        }

        [Test]
        public void triple_match_wins_triple_bet()
        {
            var rule = new TripleMatchWinTripleBet();
            var diceRollValues = new int[3] { 6, 6, 6 };

            var result = rule.GetScoreResult(diceRollValues);

            Assert.AreEqual(false, result.IsFault);
            Assert.AreEqual(3, result.Multiplier);
            Assert.AreEqual(0, result.Score);
        }

        [TestCase(4, 5, 6)]
        [TestCase(4, 6, 5)]
        [TestCase(5, 4, 6)]
        [TestCase(5, 6, 4)]
        [TestCase(6, 5, 4)]
        [TestCase(6, 4, 5)]
        public void four_five_six_win_double_bet(int dice1, int dice2, int dice3)
        {
            var rule = new FourFiveSixWinDoubleBet();
            var diceRollValues = new int[3] { dice1, dice2, dice3 };

            var result = rule.GetScoreResult(diceRollValues);

            Assert.AreEqual(false, result.IsFault);
            Assert.AreEqual(2, result.Multiplier);
            Assert.AreEqual(0, result.Score);
        }

        [Test]
        public void atleast_two_match_win_bet()
        {
            var rule = new AtleastTwoDiceMatchWinBet();
            var permutations = GetKCombsWithRept(new int[6] { 1, 2, 3, 4, 5, 6 }, 3);           

            foreach (var p in permutations)
            {
                int[] diceValues = p.ToArray();
                int expectedScore = 0;
                for (int i = 1; i <= 6; i++)
                {
                    if (diceValues.Count(x => x == i) == 2)
                    {
                        expectedScore = i;
                        break;
                    }
                }

                if (expectedScore > 0)
                {
                    var result = rule.GetScoreResult(diceValues);

                    Assert.AreEqual(false, result.IsFault);
                    Assert.AreEqual(1, result.Multiplier);
                    Assert.AreEqual(expectedScore, result.Score);
                }
            }
        }

        [Test]
        public void all_different_no_win()
        {
            var validator = new ScoreRuleValidator();
            var diceRollValues = new int[3] { 1, 4, 5 };

            var result = validator.Validate(diceRollValues);

            Assert.AreEqual(true, result.IsFault);
            Assert.AreEqual(0, result.Multiplier);
            Assert.AreEqual(0, result.Score);
        }

        static IEnumerable<IEnumerable<T>>
    GetKCombsWithRept<T>(IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombsWithRept(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
