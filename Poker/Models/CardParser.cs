using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker.Models
{
    /// <summary>
    /// Object used for card parsing and validation.
    /// </summary>
    public class CardParser
    {
        /// <summary>
        /// Parses strings into Tuple containing a cards value and suite.  Throws an ArgumentException if
        /// invalid data is found.
        /// </summary>
        /// <param name="dat">string to parse</param>
        /// <returns>A Tuple containing the resulting value and suite.</returns>
        public Tuple<int, char> ParseCard(string dat)
        {
            if (dat.Length > 4)
                throw new ArgumentException("Invalid card value.");

            char suite = dat[dat.Length - 1];
            if (suite != 'C' && suite != 'H' && suite != 'D' && suite != 'S')
                throw new ArgumentException("Unkown Suite.");

            string val = dat.Substring(0, dat.Length == 3 ? 2 : 1);
            int faceValue = 0;
            if (Int32.TryParse(val, out faceValue))
            {
                if (faceValue < 2 || faceValue > 10)
                    throw new ArgumentException("Invalid card value.");
            }
            else
            {
                switch (val)
                {
                    case "J":
                        {
                            faceValue = 11;
                            break;
                        }
                    case "Q":
                        {
                            faceValue = 12;
                            break;
                        }
                    case "K":
                        {
                            faceValue = 13;
                            break;
                        }
                    case "A":
                        {
                            faceValue = 14;
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Invalid card value.");
                        }
                }
            }
            return new Tuple<int, char>(faceValue, suite);
        }
    }
}