using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Models;

namespace Poker.Tests.Unit_Tests
{
    [TestClass]
    public class CardParserTest
    {
        CardParser parser = new CardParser();
        [TestMethod]
        [TestCategory("Unit Tests")]
        public void NumericCardTest()
        {
            Tuple<int, char> card = parser.ParseCard("2C");
            Assert.AreEqual(card.Item2, 'C');
            Assert.AreEqual(card.Item1, 2);
            card = parser.ParseCard("10H");
            Assert.AreEqual(card.Item2, 'H');
            Assert.AreEqual(card.Item1, 10);
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void FaceCardTest()
        {
            Tuple<int, char> card = parser.ParseCard("AS");
            Assert.AreEqual(card.Item2, 'S');
            Assert.AreEqual(card.Item1, 14);
            card = parser.ParseCard("KC");
            Assert.AreEqual(card.Item2, 'C');
            Assert.AreEqual(card.Item1, 13);
            card = parser.ParseCard("QH");
            Assert.AreEqual(card.Item2, 'H');
            Assert.AreEqual(card.Item1, 12);
            card = parser.ParseCard("JD");
            Assert.AreEqual(card.Item2, 'D');
            Assert.AreEqual(card.Item1, 11);
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void InvalidNumericCardTest()
        {
            Assert.IsTrue(CardCreationThrowsException("0H"));
            Assert.IsTrue(CardCreationThrowsException("-4C"));
            Assert.IsTrue(CardCreationThrowsException("133C"));
            Assert.IsTrue(CardCreationThrowsException("11H"));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void InvalidSuiteCardTest()
        {
            Assert.IsTrue(CardCreationThrowsException("3I"));
            Assert.IsTrue(CardCreationThrowsException("53"));
            Assert.IsTrue(CardCreationThrowsException("10G"));
        }

        public bool CardCreationThrowsException(string val)
        {
            bool fails = false;
            try
            {
                parser.ParseCard(val);
            }
            catch (ArgumentException ae)
            {
                fails = true;
            }
            catch (Exception ex) { }
            return fails;
        }
    }
}
