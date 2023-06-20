using ContractMakerWebApi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractMakerWebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HtmlToPdfController : Controller
    {

        private readonly ISeleniumService _seleniumService;

        public HtmlToPdfController(ISeleniumService seleniumService)
        {
            _seleniumService = seleniumService;
        }

        [HttpPost("ExportPDF")]
        public IActionResult ExportPdf([FromBody] Requ req)
        {
            var html = req.Content;
    
            var pdf = _seleniumService.ChromeDriverConvertor(html).Result;

            if (pdf == null)
            {
                return BadRequest();
            }

            return Ok(pdf);
        }
    }
    public class Requ
    {
        public string Content { get; set; }
    }
}
