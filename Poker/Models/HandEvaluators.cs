using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker.Models
{
    public interface IHandEvaluator
    {
        /// <summary>
        /// Bonus that a hand matches this Evaluator gets.
        /// </summary>
        int Bonus { get; }
        /// <summary>
        /// Evaulates an Enumerable containing cards.
        /// </summary>
        /// <param name="cards">Cards to evaluated.</param>
        /// <returns>The score found, or -1 if no matches were made.</returns>
        int Evaluate(SortedList<int, List<char>> cards);
    }

    public class SingleHighestEvaluator : IHandEvaluator
    {
        public int Bonus { get { return 0; } }
        public int Evaluate(SortedList<int, List<char>> cards)
        {
            return cards.Keys[0];
        }
    }

    public class SameSuiteCountEvaluator : IHandEvaluator
    {
        private int count;
        public int Bonus
        {
            get
            {
                int value = 0;
                if (count == 1)
                    value = 0;
                if (count == 2)
                    value = 50;
                if (count == 3)
                    value = 150;
                if (count == 4)
                    value = 350;
                return value;
            }
        }

        public SameSuiteCountEvaluator(int count)
        {
            this.count = count;
        }

        public int Evaluate(SortedList<int, List<char>> cards)
        {
            // Enumerate through the list and return the first key where the number if suites is equal to
            // the number of suites required.
            int value = cards.FirstOrDefault(x => x.Value.Count == count).Key;
            return value == 0 ? value : value;
        }
    }

    public class TwoPairHighestEvaluator : IHandEvaluator
    {
        public int Bonus { get { return 100; } }

        public int Evaluate(SortedList<int, List<char>> cards)
        {
            int pairs = cards.Count(x => x.Value.Count == 2);
            if (pairs < 2)
            {
                return 0;
            }
            return cards.FirstOrDefault(x => x.Value.Count == 2).Key;
        }
    }

    public class StraightEvaluator : IHandEvaluator
    {

        public int Bonus
        {
            get { return 200; }
        }

        public int Evaluate(SortedList<int, List<char>> cards)
        {
            int highest = cards.Keys[0];
            if (cards.Keys.Count != 5)
                return 0;
            for (int i = 1; i < cards.Keys.Count; ++i)
            {
                if (highest - cards.Keys[i] != 1)
                    return 0;

                highest = cards.Keys[i];
            }
            return cards.Keys[0];
        }
    }

    public class FlushEvaluator : IHandEvaluator
    {
        public int Bonus
        {
            get { return 250; }
        }

        public int Evaluate(SortedList<int, List<char>> cards)
        {
            char suite = cards.Values[0][0];
            for (int i = 1; i < cards.Count; ++i)
            {
                if (cards.Values[i][0] != suite)
                {
                    return 0;
                }
            }
            return cards.Keys[0];
        }
    }

    public class FullHouseEvaluator : IHandEvaluator
    {
        public int Bonus
        {
            get { return 300; }
        }

        public int Evaluate(SortedList<int, List<char>> cards)
        {
            SameSuiteCountEvaluator auxEval = new SameSuiteCountEvaluator(2);
            int val = 0;
            if (auxEval.Evaluate(cards) > 0)
            {
                auxEval = new SameSuiteCountEvaluator(3);
                val = auxEval.Evaluate(cards);
            }
            return val;
        }
    }

    public class StraightFlushEvaluator : IHandEvaluator
    {
        public int Bonus
        {
            get { return 400; }
        }

        public int Evaluate(SortedList<int, List<char>> cards)
        {
            IHandEvaluator auxEval = new FullHouseEvaluator();
            int value = 0;
            if (new StraightEvaluator().Evaluate(cards) > 0 && 
                new FlushEvaluator().Evaluate(cards) > 0)
            {
                value = cards.Keys[0];
            }
            return value;
        }
    }


}