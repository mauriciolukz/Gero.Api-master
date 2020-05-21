using Microsoft.AspNetCore.Hosting;
using System;

namespace Gero.API.Helpers
{
    public class AS400Schema
    {
        public static string GetSchema(string PREFIX)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == EnvironmentName.Staging || environment == EnvironmentName.Development)
                return $"{PREFIX}.{Constants.PRODUCTION_SCHEMA}";

            if (environment == EnvironmentName.Production)
                return $"{PREFIX}.{Constants.PRODUCTION_SCHEMA}";

            return null;
        }
    }
}
