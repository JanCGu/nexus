using Core.Interfaces;
using Core.Service;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace Config {
    public static class ChipCardGetterFactory {

        public static IChipCardGetter FromNetwork() {
            var client = new HttpClientHandler {
                Timeout = TimeSpan.FromSeconds(SettingsService.GetTimeout())
            };
            return new Retriver.ChipCardJsonRetriver(client);
        }

        public static IChipCardGetter FromStorage() {
            string storageLocation = SettingsService.GetStorageLocation();
            var storageFormatter = new BinaryFormatter();
            return new Storage.ChipCardRetriver(storageFormatter,storageLocation);
        }
    }
}
