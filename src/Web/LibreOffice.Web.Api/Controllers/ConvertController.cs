using System.IO;
using System.Threading.Tasks;
using LibreOffice.Packages.Api.Models;
using LibreOffice.Services.Api.Conversions;
using LibreOffice.Services.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibreOffice.Web.Api.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    
    public class ConvertController : Controller
    {
        private readonly ILogger<ConvertController> _logger;
        private readonly IConversionService _conversionService;

        /// <inheritdoc />
        public ConvertController(ILogger<ConvertController> logger, IConversionService conversionService)
        {
            _logger = logger;
            _conversionService = conversionService;
        }

        /// <summary>
        /// Converts a file to a different format
        /// </summary>
        /// <param name="fileConversionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Convert([FromForm] FileConversionRequestForm fileConversionRequest)
        {
            _logger.LogInformation($"Converting {fileConversionRequest.Document.FileName} to {fileConversionRequest.FileFormat}");
            await using var ms = new MemoryStream();
            await fileConversionRequest.Document.CopyToAsync(ms);
            var result = await _conversionService.Convert(new FileConversionRequest
            {
                FileName = fileConversionRequest.Document.FileName,
                FileContents = ms.ToArray(),
                ConversionExtension = fileConversionRequest.FileFormat
            });
            await ms.ReadAsync(result.FileContents);
            return File(result.FileContents, "application/octet-stream", result.FileName);
        }
    }
}