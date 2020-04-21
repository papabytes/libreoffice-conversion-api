using System.Threading.Tasks;
using LibreOffice.Services.Api.Models;

namespace LibreOffice.Services.Api.Conversions
{
    public interface IConversionService
    {
        Task<FileConversionResponse> Convert(FileConversionRequest fileConversionRequest);
    }
}