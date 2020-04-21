using LibreOffice.Services.Api.Conversions;
using Microsoft.Extensions.DependencyInjection;

namespace LibreOffice.Services.Api
{
    public static class DependencyInjection
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IConversionService, ConversionService>();
        }
    }
}