using Microsoft.AspNetCore.Http;

namespace LibreOffice.Packages.Api.Models
{
    /// <summary>
    /// File Conversion request model
    /// </summary>
    public class FileConversionRequestForm
    {
        /// <summary>
        /// The document to convert
        /// </summary>
        public IFormFile Document { get; set; }

        /// <summary>
        /// The desired extension (without '.') i.e: pdf
        /// </summary>
        public string FileFormat { get; set; }
    }
}