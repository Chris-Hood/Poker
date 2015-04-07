using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker.Models
{
    /// <summary>
    /// Evaluates given hands.
    /// </summary>
    public class HandValueComputer
    {
        /// <summary>
        /// List of Evaluators to use, going from highest value to lowest value.
        /// </summary>
        private static List<IHandEvaluator> evaluators = new List<IHandEvaluator>()
        {
            new StraightFlushEvaluator(),
            new SameSuiteCountEvaluator(4),
            new FullHouseEvaluator(),
            new FlushEvaluator(),
            new StraightEvaluator(),
            new SameSuiteCountEvaluator(3),
            new TwoPairHighestEvaluator(),
            new SameSuiteCountEvaluator(2),
            new SingleHighestEvaluator(),
        };

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

        /// <summary>
        /// Evaluates a given hand of cards.
        /// </summary>
        /// <param name="cards">5 cards to evaluate.</param>
        /// <returns>Value of the given cards.</returns>
        public int ComputeValue(IEnumerable<Tuple<int, char>> cards)
        {
            SortedList<int, List<char>> cardData =
                new SortedList<int,List<char>>(new ReverseComparer());
            foreach (Tuple<int, char> card in cards)
            {
                if (!cardData.ContainsKey(card.Item1))
                    cardData.Add(card.Item1, new List<char>() { card.Item2 });
                else
                    cardData[card.Item1].Add(card.Item2);
            }

            int i = 0;
            int thisValue = 0;
            while (thisValue == 0 && i < evaluators.Count)
            {
                thisValue = evaluators[i].Evaluate(cardData);
                if (thisValue > 0)
                    thisValue += evaluators[i].Bonus;
                ++i;
            }
            return thisValue;
        }
    }
}