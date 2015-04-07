using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.Controllers
{
    public class PokerController : ApiController
    {
        /// <summary>
        /// Computes the winner of a Round of poker.
        /// </summary>
        /// <param name="hands">JSON data structure containing player names and hands with each hand consisting of 5 cards.</param>
        /// <returns>The winning player.</returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]Round hands)
        {
            // A lot of this validation happens client side so validating
            // here is more for the sake of thoroughness (Not assuming the
            // client is nice even though in this case I control the client.)
            // than anything else.

            if (hands == null || hands.Hands == null || hands.Hands.Count == 0)
            {
                return BadRequest("Data is missing.");
            }

            // Perform data validation for number of cards.
            if (!hands.Hands.TrueForAll(x => x.Hand != null && x.Hand.Count == 5))
                return BadRequest("Wrong number of cards found in someone's hand.");

            HashSet<string> playersPresent = new HashSet<string>();
            if (!hands.Hands.TrueForAll(x => x.Name != null && x.Name != "" && playersPresent.Add(x.Name)))
                return BadRequest("A player has no name, or there are two players with the same name!");

            HashSet<string> cardsPresent = new HashSet<string>();
            // Validate no duplicate players and no duplicate cards.
            foreach (Play play in hands.Hands)
            {
                if (!play.Hand.TrueForAll(x => cardsPresent.Add(x)))
                    return BadRequest("A card was either missing or a duplicate.");
            }

            // Enumerate through all plays, picking out the highest card, in the case of a draw the
            // first player is considered the winner.
            HandValueComputer computer = new HandValueComputer();
            CardParser parser = new CardParser();
            string winPlayer = "";
            int winValue = 0;
            foreach (Play p in hands.Hands)
            {
                try
                {
                    IEnumerable<Tuple<int, char>> cards = p.Hand.Select(x => parser.ParseCard(x));
                    int val = computer.ComputeValue(cards);
                    if (val > winValue)
                    {
                        winPlayer = p.Name;
                        winValue = val;
                    }
                }
                catch (ArgumentException ae)
                {
                    // Return a bad request in the case where a card cannot be parsed.
                    return BadRequest("Bad card found.");
                }
            }
            return Ok(winPlayer);
        }
    }
}
