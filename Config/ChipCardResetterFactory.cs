using Core.Interfaces;
using Core.Service;

namespace Config {
    public static class ChipCardResetterFactory {

        /// <summary>
        /// Initalis the Resetter with a network getter and storage getter and setter.
        /// </summary>
        /// <param name="tries">Passes the tries to the resetter, which indicates how often
        /// a failed actioun should be repeted before an exception is thrown. Default is -1,
        /// which means to use the App.config.</param>
        public static IChipCardResetter NetWorkStorageResetter(int tries = -1) {
            var storageGetter = ChipCardGetterFactory.FromStorage();
            var networkGetter = ChipCardGetterFactory.FromNetwork();
            var storageSetter = ChipCardSetterFactory.ToStorage();

            return new ChipCardResetter(storageSetter, storageGetter, networkGetter, tries);
        }

    }
}
