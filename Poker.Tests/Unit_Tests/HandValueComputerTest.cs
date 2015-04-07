using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Models;
using System.Collections.Generic;

namespace Poker.Tests.Unit_Tests
{
    [TestClass]
    public class HandValueComputerTest
    {
        private HandValueComputer computer = new HandValueComputer();

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void SingleTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(4, 'C'),
                new Tuple<int, char>(3, 'D'),
                new Tuple<int, char>(9, 'H'),
                new Tuple<int, char>(5, 'H'),
                new Tuple<int, char>(8, 'C'),
            };
            Assert.AreEqual(9, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void PairTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(4, 'C'),
                new Tuple<int, char>(4, 'H'),
                new Tuple<int, char>(9, 'D'),
                new Tuple<int, char>(5, 'H'),
                new Tuple<int, char>(8, 'C'),
            };
            Assert.AreEqual(54, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void TwoPairsTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(4, 'C'),
                new Tuple<int, char>(4, 'H'),
                new Tuple<int, char>(9, 'D'),
                new Tuple<int, char>(9, 'H'),
                new Tuple<int, char>(8, 'C'),
            };
            Assert.AreEqual(109, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void ThreeOfAKindTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(4, 'C'),
                new Tuple<int, char>(4, 'H'),
                new Tuple<int, char>(4, 'D'),
                new Tuple<int, char>(5, 'H'),
                new Tuple<int, char>(8, 'C'),
            };
            Assert.AreEqual(154, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void StraightTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(5, 'C'),
                new Tuple<int, char>(6, 'H'),
                new Tuple<int, char>(7, 'D'),
                new Tuple<int, char>(8, 'H'),
                new Tuple<int, char>(9, 'C'),
            };
            Assert.AreEqual(209, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void FlushTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(5, 'C'),
                new Tuple<int, char>(7, 'C'),
                new Tuple<int, char>(2, 'C'),
                new Tuple<int, char>(11, 'C'),
                new Tuple<int, char>(9, 'C'),
            };
            Assert.AreEqual(261, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void FullHouseTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(5, 'C'),
                new Tuple<int, char>(5, 'H'),
                new Tuple<int, char>(8, 'H'),
                new Tuple<int, char>(8, 'D'),
                new Tuple<int, char>(8, 'S'),
            };
            Assert.AreEqual(308, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void FourOfAKindTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(5, 'C'),
                new Tuple<int, char>(8, 'C'),
                new Tuple<int, char>(8, 'H'),
                new Tuple<int, char>(8, 'D'),
                new Tuple<int, char>(8, 'S'),
            };
            Assert.AreEqual(358, computer.ComputeValue(cards));
        }

        [TestMethod]
        [TestCategory("Unit Tests")]
        public void StraightFlushTest()
        {
            List<Tuple<int, char>> cards = new List<Tuple<int, char>> {
                new Tuple<int, char>(5, 'C'),
                new Tuple<int, char>(6, 'C'),
                new Tuple<int, char>(7, 'C'),
                new Tuple<int, char>(8, 'C'),
                new Tuple<int, char>(9, 'C'),
            };
            Assert.AreEqual(409, computer.ComputeValue(cards));
        }

    }
}
