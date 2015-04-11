using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Poker.Models
{
    public class DeckGenerator
    {
        public string GenerateDeckXML()
        {
            List<string> suites = new List<string>() { "C", "S", "H", "D" };
            List<string> values = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            var cards = from s in suites
                        from v in values
                        select v + s;
            StringBuilder deck = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(deck))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Cards");

                foreach (string s in cards)
                {
                    writer.WriteElementString("Card", s);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return deck.ToString();
        }
    }
}