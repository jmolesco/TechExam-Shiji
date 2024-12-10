using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShijiGroup.Models;
using System.Collections.Concurrent;
using System.Linq;
namespace ShijiGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SequenceController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, int> ClientSequenceNumbers = new ConcurrentDictionary<string, int>();
        private readonly SequenceContext sequenceContext;
        public SequenceController(SequenceContext _sequenceContext)
        {
            sequenceContext = _sequenceContext;
        }
        // GET: api/sequence/getnextid?Clientid={clientId}
        //[HttpGet("getnextid")]
        //public async Task<IActionResult> GetNextId([FromQuery] string clientid)
        //{
        //    if (string.IsNullOrEmpty(clientid))
        //    {
        //        return BadRequest("Clientid is required.");
        //    }

        //    // Get the current sequence number for the client (or 0 if it doesn't exist)
        //    int currentSeq = ClientSequenceNumbers.GetOrAdd(clientid, 0);

        //    // Increment the sequence number for the client
        //    int nextSeq = currentSeq + 1;
        //    await Task.Delay(2000);
        //    ClientSequenceNumbers[clientid] = nextSeq;

        //    return Ok(nextSeq);
        //}


        [HttpGet("getnextid")]
        public async Task<IActionResult> GetNextId([FromQuery] string clientid)
        {
            var sequence = await sequenceContext.Sequences.FirstOrDefaultAsync(x => x.Name == clientid);
            var sequenceRecord = new Sequence();
            if (sequence == null)
            {
                sequenceRecord = new Sequence { Name = clientid, Number=1 };
                sequenceContext.Sequences.Add(sequenceRecord);
                sequenceContext.SaveChanges();
            }
            else
            {

                sequence.Number = sequence.Number + 1;
                sequenceContext.Sequences.Update(sequence);
                sequenceContext.SaveChanges();
            }
            await Task.Delay(2000);
            return Ok(sequence?.Number ?? 1);
        }

        [HttpGet("reset")]
        public async Task<IActionResult> Reset()
        {
            sequenceContext.Sequences.RemoveRange(sequenceContext.Sequences);
            sequenceContext.SaveChanges();
            return Ok("Record were reset.");
        }

    }
}
