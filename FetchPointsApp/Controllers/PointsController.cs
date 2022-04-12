using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;
using FetchPointsApp.Models;
using System.Net;

namespace FetchPointsApp.Controllers
{
    /// <summary>
    /// Controller class for FetchPoints endpoints
    /// </summary>
    [ApiController]
    public class PointsController : Controller
    {
        /// <summary>
        /// Singleton form of data structure class
        /// </summary>
        public static PointsManipulator _points = new PointsManipulator();
        private readonly ILogger<PointsController> _logger;

        /// <summary>
        /// Constructor of controller for to instantiate loggers
        /// </summary>
        /// <param name="logger">Logger object</param>
        public PointsController(ILogger<PointsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Endpoint that attempts to add pass in JSON object to data structure of Payers
        /// </summary>
        /// <param name="jObject">JSON Object</param>
        /// <returns>HTTP Status</returns>
        [HttpPost, Route("api/[controller]/addtransaction")]
        public IActionResult AddTransaction([FromBody] JObject jObject)
        {
            Payers p;
            try {  p = JsonConvert.DeserializeObject<Payers>(jObject.ToString()); }
            catch (JsonException e)
            {
                return BadRequest("Incorrectly formatted request. Must be in format {\n \"Payer\":\"NAME\",\n\"Points\":\"000\",\"Timestamp\":\"YYYY-MM-DD HH:MM:SS\"}");
            }
            _points.AddTransaction(p);
            return Ok(p.GetNamePoints() + " has been added!");
        }

        /// <summary>
        /// Endpoint that attempts to spend a passed in amount of points in the existing points data structure
        /// </summary>
        /// <param name="jObject">JSON Object</param>
        /// <returns>HTTP Status</returns>
        [HttpPost, Route("api/[controller]/spendpoints")]
        public IActionResult SpendPoints([FromBody] JObject jObject)
        {
            int points;
            try { points = JsonConvert.DeserializeObject<dynamic>(jObject.ToString()).points; }
            catch (JsonException e)
            {
                return BadRequest("Incorrectly formatted request. Must be in format {\"Points\": \"000\"}");
            }
                
            return Ok(_points.SpendPoints(points));
        }

        /// <summary>
        /// Endpoint that fetches all payers in the system and their active balance
        /// </summary>
        /// <returns>HTTP Status</returns>
        [HttpGet, Route("api/[controller]/pointsbalance")]
        public IActionResult PointsBalance()
        {
            return Ok(_points.GetBalance());
        }
    }
}
