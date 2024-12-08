using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ShijiGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SequenceController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, int> ClientSequenceNumbers = new ConcurrentDictionary<string, int>();

        // GET: api/sequence/getnextid?Clientid={clientId}
        [HttpGet("getnextid")]
        public IActionResult GetNextId([FromQuery] string clientid)
        {
            if (string.IsNullOrEmpty(clientid))
            {
                return BadRequest("Clientid is required.");
            }

            // Get the current sequence number for the client (or 0 if it doesn't exist)
            int currentSeq = ClientSequenceNumbers.GetOrAdd(clientid, 0);

            // Increment the sequence number for the client
            int nextSeq = currentSeq + 1;
            ClientSequenceNumbers[clientid] = nextSeq;

            return Ok(nextSeq);
        }

        [HttpGet("resetcache")]
        public IActionResult ResetCache()
        {
            ClientSequenceNumbers.Clear();  
            return Ok("Cache Resetted Successfully!");
        }

    }
}
