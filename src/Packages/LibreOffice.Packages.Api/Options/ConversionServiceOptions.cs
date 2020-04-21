﻿namespace LibreOffice.Packages.Api.Options
{
    /// <summary>
    /// IOptions for Conversion Service configuration
    /// </summary>
    public class ConversionServiceOptions
    {
        /// <summary>
        /// Endpoint at which the Conversion Api is being served
        /// </summary>
        public string Url { get; set; }
    }
}