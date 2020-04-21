using System.Threading.Tasks;

namespace LibreOffice.Packages.Api.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConversionApiService
    {
        /// <summary>
        /// Converts a file to a different format
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileContents"></param>
        /// <param name="extension">Desired format extension i.e.: 'pdf'</param>
        /// <returns></returns>
        Task<byte[]> Convert(string fileName, byte[] fileContents, string extension);
    }
}