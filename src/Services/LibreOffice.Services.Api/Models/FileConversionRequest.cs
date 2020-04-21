namespace LibreOffice.Services.Api.Models
{
    public class FileConversionRequest : FileConversionResponse
    {
        /// <summary>
        /// Target extension to convert to i.e.: "pdf" "png" "docx"
        /// </summary>
        public string ConversionExtension { get; set; }
    }
}