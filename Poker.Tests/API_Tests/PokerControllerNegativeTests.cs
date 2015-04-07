using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Controllers;
using Poker.Models;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace Poker.Tests.API_Tests
{
    [TestClass]
    public class PokerControllerNegativeTests
    {
        [TestMethod]
        [TestCategory("API Tests")]
        public void DuplicateNameTest()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2C", "3H", "4S", "8C", "AH" } });
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2H", "3D", "5S", "9C", "KD" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                    "Duplicate player names should return a BadRequestErrorMessageResult");

        }

        [TestMethod]
        [TestCategory("API Tests")]
        public void DuplicateCardTest()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2C", "3H", "4S", "8C", "AH" } });
            r.Hands.Add(new Play() { Name = "Bill", Hand = new List<string>() { "2H", "3D", "5S", "9C", "AH" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                    "Duplicate cards should return a BadRequestErrorMessageResult");
        }

        [TestMethod]
        [TestCategory("API Tests")]
        public void WrongCardCountTest()
        {
            Round r = new Round();
            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bob", Hand = new List<string>() { "2C", "3H", "4S", "8C", "AH", "2S" } });
            r.Hands.Add(new Play() { Name = "Bill", Hand = new List<string>() { "2H", "3D", "5S", "9C", "KD" } });
            var actionResult = new PokerController().Post(r);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                    "Wrong card count should return a BadRequestErrorMessageResult");
        }

        [TestMethod]
        [TestCategory("API Tests")]
        public void MissingDataTest()
        {
            //Missing data should cause the API to return a bad request, not crash and burn.
            PokerController p = new PokerController();
            var actionResult = p.Post(null);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                    "No data should result in a BadRequestErrorMessageResult");
            Round r = new Round();
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                    "Missing hand data should result in a BadRequestErrorMessageResult");

            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = "Bob", Hand = null });
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                    "Missing hand data should result in a BadRequestErrorMessageResult");
            r.Hands = new List<Play>();
            r.Hands.Add(null);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                   "Missing hand data should result in a BadRequestErrorMessageResult");

            r.Hands = new List<Play>();
            r.Hands.Add(new Play() { Name = null, Hand = new List<string>() { "2C", "3H", "4S", "8C", "AH", "2S" } });
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult),
                                   "Missing hand data should result in a BadRequestErrorMessageResult");
        }
    }
}