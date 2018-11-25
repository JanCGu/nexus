using Core.Interfaces;
using Core.Service;
using Storage;
using System.Runtime.Serialization.Formatters.Binary;

namespace Config {
    public static class ChipCardWhereGetterFactory {
        public static IChipCardWhereGetter WhereFromStorage() {

            string storageLocation = SettingsService.GetStorageLocation();
            var storageFormatter = new BinaryFormatter();
            return new ChipCardRetriver(storageFormatter, storageLocation);
        }
    }
}
