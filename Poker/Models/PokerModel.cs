/*
 * These classes are used by ASP.NET to parse incoming JSON.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker.Models
{
    /// <summary>
    /// Contains data for a single play.
    /// </summary>
    public class Play 
    {
        public string Name { get; set; }
        public List<String> Hand { get; set; }
    }

    /// <summary>
    /// Contains all Plays for this round.
    /// </summary>
    public class Round
    {
        public List<Play> Hands { get; set; }
    }
}