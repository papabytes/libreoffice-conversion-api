using LibreOffice.Packages.Api.Clients;
using LibreOffice.Packages.Api.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibreOffice.Packages.Api
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures ConversionServiceOptions from AppSettings section defined by sectionName
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="sectionName"></param>
        public static void AddConversionApiService(this IServiceCollection services,
            IConfiguration configuration, string sectionName = "ConversionService")
        {
            services.Configure<ConversionServiceOptions>(configuration.GetSection(sectionName));
            services.AddScoped<IConversionApiService, ConversionApiService>();
        }
    }
}