using Core.Interfaces;
using Core.Service;
using Storage;
using System.Runtime.Serialization.Formatters.Binary;

namespace Config {
    public static class ChipCardSetterFactory {
        public static IChipCardSetter ToStorage() {
            var storageDeleter = ChipCardDeleterFactory.CompleteStorageDeleter();
            string storageLocation = SettingsService.GetStorageLocation();
            var storageFormat = new BinaryFormatter();
            return new ChipCardStorer(storageLocation, storageFormat, storageDeleter);
        }
    }
}
