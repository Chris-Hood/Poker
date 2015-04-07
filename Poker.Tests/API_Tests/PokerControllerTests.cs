using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Poker.Models;
using System.Web.Http.Results;
using Poker.Controllers;

namespace Poker.Tests.API_Tests
{
    [TestClass]
    public class PokerControllerTests
    {
        [TestMethod]
        [TestCategory("API Tests")]
        public void TestResult()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bill", Hand = new List<string>() { "2C", "3H", "4S", "8C", "AH" } });
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2H", "3D", "5S", "9C", "KD" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>),
                                    "Should return an OkResult under normal conditions.");
        }

        [TestMethod]
        [TestCategory("API Tests")]
        public void TestCorrectWinner()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bill", Hand = new List<string>() { "2C", "3H", "4S", "8C", "AH" } });
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2H", "3D", "5S", "9C", "KD" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>),
                                    "Should return an OkResult under normal conditions.");
            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Bill", contentResult.Content,
                            "Correct winner should be returned.");
        }

        [TestMethod]
        [TestCategory("API Tests")]
        public void TestCorrectWinnerComplexHands()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bill", Hand = new List<string>() { "2C", "3D", "4S", "8C", "AH" } });
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2H", "3H", "4H", "5H", "6H" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>),
                                    "Duplicate player names should return an OkResult under normal conditions.");
            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Bob", contentResult.Content,
                            "Correct winner should be returned when hand is complex.");
        }

        [TestMethod]
        [TestCategory("API Tests")]
        public void TestSingleHand()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bill", Hand = new List<string>() { "2C", "3D", "4S", "8C", "AH" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>),
                                    "Duplicate player names should return an OkResult when passed a single hand.");
            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Bill", contentResult.Content,
                            "API should behave correctly when given a single player.");
        }

        [TestMethod]
        [TestCategory("API Tests")]
        public void TestThreeHand()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bill", Hand = new List<string>() { "2C", "3D", "4S", "8C", "AH" } });
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2H", "3C", "4D", "5H", "7H" } });
            r.Hands.Add(new Play() { Name = "Ted", Hand = new List<string>() { "KH", "AC", "AS", "7S", "10H" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>),
                                    "Duplicate player names should return an OkResult when passed three hands.");
            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Ted", contentResult.Content,
                            "API should behave correctly when given 3 players.");
        }
    }
}