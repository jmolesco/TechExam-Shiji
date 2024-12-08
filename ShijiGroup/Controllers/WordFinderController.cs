using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShijiGroup.Infrastracture.Interface;
using ShijiGroup.Infrastracture.Service;

namespace ShijiGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFinderController : ControllerBase
    {
        private readonly IWordFinderService wordFinderService;
        public WordFinderController(IWordFinderService _wordFinderService)
        {
            wordFinderService = _wordFinderService;
        }

        [HttpGet("boardmatrix/display")]
        public async Task<IActionResult> DisplayBoard()
        {
            var result =await wordFinderService.GetMatrices();
            return Ok(result);
        }

        [HttpPost("boardmatrix/findwords")]
        public async Task<IActionResult> FindWords([FromBody] List<string> request)
        {

            //sample input ["chill", "wind", "snow", "cold"]
            if (request == null || request.Count() == 0)
                return BadRequest("Invalid input.");

            var result =await wordFinderService.FindWords(request);
            return Ok(result);
        }
    }
}
