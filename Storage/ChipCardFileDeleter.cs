using Core.Interfaces;
using Core.Service;
using System;
using System.IO;

namespace Storage {
    public class ChipCardFileDeleter : IChipCardDeleter {

        private readonly string storageLocation;

        public ChipCardFileDeleter(string StorageLocation = null) {
            storageLocation = SettingsService.GetStorageLocationIfNullOrWhiteSpace(StorageLocation);
        }

        /// <summary>
        /// Deletes the file at the storageLocation.
        /// </summary>
        /// <returns>Is True if no file at the storageLocation exists after the operation otherwise false.</returns>
        public bool DeleteAll() {
            try {
                if (File.Exists(storageLocation)) {
                    File.Delete(storageLocation);
                }
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }
}
