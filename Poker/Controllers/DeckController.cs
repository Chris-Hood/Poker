using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Poker.Controllers
{
    public class DeckController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new DeckGenerator().GenerateDeckXML());
        }
    }
}