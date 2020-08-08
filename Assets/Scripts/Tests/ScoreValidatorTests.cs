using System.Collections;
using System.Collections.Generic;
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
            var dice = new Dice[3]
            {
                new Dice() { sideValue = 1},
                new Dice() { sideValue = 1},
                new Dice() { sideValue = 1},
            };

            var score = rule.GetScore(dice, 100);

            Assert.AreEqual(-300, score);
        }

        [Test]
        public void one_two_three_lose_double_bet()
        {
            var rule = new OneTwoThreeLoseDoubleBet();
            var dice = new Dice[3]
            {
                new Dice() { sideValue = 1},
                new Dice() { sideValue = 2},
                new Dice() { sideValue = 3},
            };

            var score = rule.GetScore(dice, 100);

            Assert.AreEqual(-200, score);
        }

        [Test]
        public void triple_match_wins_triple_bet()
        {
            var rule = new TripleMatchWinTripleBet();
            var dice = new Dice[3]
            {
                new Dice() { sideValue = 6},
                new Dice() { sideValue = 6},
                new Dice() { sideValue = 6},
            };

            var score = rule.GetScore(dice, 100);

            Assert.AreEqual(300, score);
        }

        [Test]
        public void four_five_six_win_double_bet()
        {
            var rule = new FourFiveSixWinDoubleBet();
            var dice = new Dice[3]
            {
                new Dice() { sideValue = 4},
                new Dice() { sideValue = 5},
                new Dice() { sideValue = 6},
            };

            var score = rule.GetScore(dice, 100);

            Assert.AreEqual(200, score);
        }

        [Test]
        public void atleast_two_match_win_bet()
        {
            var rule = new AtleastTwoDiceMatchWinBet();
            var dice = new Dice[3]
            {
                new Dice() { sideValue = 2},
                new Dice() { sideValue = 2},
                new Dice() { sideValue = 1},
            };

            var score = rule.GetScore(dice, 100);

            Assert.AreEqual(100, score);
        }

        [Test]
        public void all_different_no_win()
        {
            var validator = new ScoreRuleValidator();
            var dice = new Dice[3]
            {
                new Dice() { sideValue = 1},
                new Dice() { sideValue = 5},
                new Dice() { sideValue = 4},
            };

            var score = validator.Validate(dice, 100);

            Assert.AreEqual(0, score);
        }

        //// A Test behaves as an ordinary method
        //[Test]
        //public void ScoreValidatorTestsSimplePasses()
        //{
        //    // Use the Assert class to test conditions
        //}

        //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        //// `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator ScoreValidatorTestsWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
