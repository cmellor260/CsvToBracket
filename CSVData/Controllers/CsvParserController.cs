using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace CSVData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvParserController : ControllerBase
    {
        private readonly ILogger<CsvParserController> _logger;

        public CsvParserController(ILogger<CsvParserController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "FormatCsvString")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Post(string inputString)
        {
            try
            {
                ICsvParser csvParser = new CsvParser(_logger);
                return this.Ok(csvParser.CsvToBracketSeperation(inputString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "InternalServerError");
            }
        }
    }
}
