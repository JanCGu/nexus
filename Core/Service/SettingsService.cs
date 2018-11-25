using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Core.Service {

    /// <summary>
    /// Reads the Settings from an App.config file.
    /// </summary>
    public class SettingsService {

        public static string GetStorageLocation(IConfigurationRoot configuration = null) {
            return GetString("StorageLocation", configuration);
        }

        public static string GetChipCardSourceURL(IConfigurationRoot configuration = null) {
            return GetString("ChipCardGetUrl", configuration);
        }

        public static IConfigurationRoot GetConfiguration() {
            try {
                return new ConfigurationBuilder()
                 .AddJsonFile("config.json")
                 .Build();
            }
            catch (Exception ex) {
                throw new KeyNotFoundException("Could not retrive the key in App.config", ex);
            }
        }

        public static int GetTries(IConfiguration configuration = null) {
            return GetInt("NetWorkRetrievTries", configuration);
        }

        public static double GetTimeout(IConfiguration configuration = null) {
            return GetDouble("Timeouts", configuration);
        }

        private static string GetString(string variable, IConfiguration configuration) {
            if (configuration == null)
                configuration = GetConfiguration();
            return configuration[variable] ?? throw new KeyNotFoundException($"Expected a {variable} Key in the App.config");
        }
        private static double GetDouble(string variable, IConfiguration configuration) {
            return double.Parse(GetString(variable, configuration));
        }

        private static int GetInt(string variable, IConfiguration configuration) {
            return int.Parse(GetString(variable, configuration));
        }

        /// <summary>
        /// Returns the StorageLocation if it is not null or whitespace otherwiese
        /// the StorageLocation in App.config will be returned.
        /// </summary>
        public static string GetStorageLocationIfNullOrWhiteSpace(string StorageLocation, IConfigurationRoot configuration = null) {
            return string.IsNullOrWhiteSpace(StorageLocation) ? GetStorageLocation(configuration) : StorageLocation;
        }
    }
}
