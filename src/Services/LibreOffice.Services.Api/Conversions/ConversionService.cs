using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using LibreOffice.Services.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LibreOffice.Services.Api.Conversions
{
    public class ConversionService : IConversionService
    {
        private readonly ILogger<IConversionService> _logger;
        private readonly IConfiguration _configuration;

        public ConversionService(ILogger<IConversionService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<FileConversionResponse> Convert(
            FileConversionRequest fileConversionRequest)
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conversions");

            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            var fileInfo = new FileInfo(fileConversionRequest.FileName);
            //original file id
            var fileId = Guid.NewGuid();

            var originalConversionFileName = $"{fileId}{fileInfo.Extension}";

            await File.WriteAllBytesAsync(Path.Combine(baseDir, originalConversionFileName),
                fileConversionRequest.FileContents);

            var resultingFileName = $"{fileId}.{fileConversionRequest.ConversionExtension}";

            using var process = new Process
            {
                StartInfo =
                {
                    FileName = _configuration["SOFFICE_BIN_PATH"],
                    Arguments =
                        $"--headless --convert-to {fileConversionRequest.ConversionExtension.ToLowerInvariant()} {originalConversionFileName} --outdir {baseDir}",
                    WorkingDirectory = baseDir,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
            process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
            
            _logger.LogInformation("starting");
            
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            var exited = process.WaitForExit(1000 * 10); //wait up to 10 seconds
          
            _logger.LogInformation($"exit {exited}");

            //delete original file
            File.Delete(Path.Combine(baseDir, originalConversionFileName));
            try
            {
                var resultingFileContents = await File.ReadAllBytesAsync(Path.Combine(baseDir, resultingFileName));

                //delete converted
                File.Delete(Path.Combine(baseDir, resultingFileName));
                return new FileConversionResponse
                {
                    FileContents = resultingFileContents,
                    FileName =
                        $"{fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf("."))}.{fileConversionRequest.ConversionExtension}"
                };
            }
            catch (FileNotFoundException)
            {
                var message =
                    $"Conversion not supported [{fileInfo.Extension.ToUpperInvariant()}] => [.{fileConversionRequest.ConversionExtension.ToUpperInvariant()}]";
                _logger.LogError(message);
                throw new Exception(message);
            }
        }
    }
}