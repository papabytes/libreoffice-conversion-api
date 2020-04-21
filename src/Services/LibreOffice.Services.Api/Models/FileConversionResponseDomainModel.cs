namespace LibreOffice.Services.Api.Models
{
    public class FileConversionResponse
    {
        public string FileName { get; set; }

        public byte[] FileContents { get; set; }
    }
}