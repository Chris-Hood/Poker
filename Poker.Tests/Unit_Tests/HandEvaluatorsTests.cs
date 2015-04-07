using System;
using Poker.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Poker.Tests.Unit_Tests
{
    [TestClass]
    public class HandEvaluatorsTests
    {
        /// <summary>
        /// Reverse Comparer is used to make sure that the data structure is sorted in descending order
        /// so that the highest value will always be in the 0 position.
        /// </summary>
        class ReverseComparer : IComparer<int>
        {

            public int Compare(int x, int y)
            {
                return y.CompareTo(x);
            }
        }
        [TestMethod]
        [TestCategory("Unit Tests")]
        public void StraightTest()
        {
            SortedList<int, List<char>> cards = new SortedList<int, List<char>>(new ReverseComparer()) {
                {6, new List<char>() {'C'} },
                {7, new List<char>() {'H'} },
                {8, new List<char>() {'S'} },
                {9, new List<char>() {'D'} },
                {10, new List<char>() {'C'} }
            };
            IHandEvaluator eval = new StraightEvaluator();
            Assert.AreNotEqual(0, eval.Evaluate(cards));
            cards = new SortedList<int, List<char>>(new ReverseComparer()) {
                {5, new List<char>() {'C'} },
                {7, new List<char>() {'H'} },
                {8, new List<char>() {'S'} },
                {9, new List<char>() {'D'} },
                {10, new List<char>() {'C'} }
            };
            Assert.AreEqual(0, eval.Evaluate(cards));
        }
        
    }
}
