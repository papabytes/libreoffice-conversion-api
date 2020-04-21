using System;
using System.Net.Http;
using System.Threading.Tasks;
using LibreOffice.Packages.Api.Models;
using LibreOffice.Packages.Api.Options;
using Microsoft.Extensions.Options;

namespace LibreOffice.Packages.Api.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class ConversionApiService :  IConversionApiService
    {
        private readonly ConversionServiceOptions _conversionServiceOptions;
        private const string ConversionPath = "api/convert";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionClientOptions"></param>
        public ConversionApiService(IOptions<ConversionServiceOptions> conversionClientOptions)
        {
            _conversionServiceOptions = conversionClientOptions.Value;
        }

        public async Task<byte[]> Convert(string fileName, byte[] fileContents, string extension)
        {
            using var client = new HttpClient {BaseAddress = new Uri(_conversionServiceOptions.Url)};
            
            var content = new MultipartFormDataContent
            {
                {new ByteArrayContent(fileContents), nameof(FileConversionRequestForm.Document), fileName},
                {new StringContent(extension), nameof(FileConversionRequestForm.FileFormat)}
            };

            var response = await client.PostAsync(ConversionPath, content);

            if (!response.IsSuccessStatusCode)
            {
                throw ConvertResponseToException(response);
            }

            var resultBytes = await response.Content.ReadAsByteArrayAsync();
            return resultBytes;
        }

        private Exception ConvertResponseToException(HttpResponseMessage response)
        {
            var message =
                $"Conversion request to {response.RequestMessage.RequestUri} failed. StatusCode: {response.StatusCode.ToString()}";

            throw new Exception(message);
        }

    }
}