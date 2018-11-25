using Core.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service {
    public class ChipCardResetter : IChipCardResetter {

        private readonly IChipCardSetter storageSetter;
        private readonly IChipCardGetter storageGetter;
        private readonly IChipCardGetter sourceGetter;

        private readonly int tries;

        /// <summary>
        /// Initalises ChipCardResetter with the storage and source interface.
        /// </summary>
        /// <param name="storageSetter"></param>
        /// <param name="networkGetter"></param>
        /// <param name="tries">The number of tries to get retrive the json and store it to the location.
        /// If it is smaller than 0 it is retrieved from the App.config in NetWorkReetrieveTries.</param>
        public ChipCardResetter(IChipCardSetter storageSetter, IChipCardGetter storageGetter, IChipCardGetter sourceGetter, int tries = -1) {
            this.storageSetter = storageSetter ?? throw new ArgumentNullException(nameof(storageSetter));
            this.storageGetter = storageGetter ?? throw new ArgumentNullException(nameof(storageGetter));
            this.sourceGetter = sourceGetter ?? throw new ArgumentNullException(nameof(sourceGetter));
            this.tries = tries;
            if (tries < 0) {
                tries = SettingsService.GetTries();
            }
        }

        /// <summary>
        /// Uses the networkGetter to get the chipcards they are than passed to the storageSetter.
        /// This is the set maxium amoutn of times. If it is 0 it is still tried once.
        /// </summary>
        /// <returns>True if the storage location is overwritten.
        /// Returns false if no Object was read and there skips the delete.</returns>
        public async Task<bool> Reset() {

            int currentTry = 1;
            var finalException = new Exception();
            do {
                try {
                    var toStore = await sourceGetter.All();
                    var toDelete = new HashSet<IChipCard>();
                    bool noStorage = false;
                    try {
                        var storageTask = storageGetter.All();
                        storageTask.Wait();
                        toDelete = storageTask.Result;
                    }
                    catch (Exception) {
                        noStorage = true;
                    }
                    if (!toStore.Any())
                        return false;
                    var deletedElements = await storageSetter.Delete(toDelete);
                    if (!deletedElements.Any() && !noStorage) {
                        finalException = new SystemException("Couldn't delete the storage object.");
                        continue;
                    }
                    var setted = await storageSetter.Insert(toStore);
                    if (setted.Count == toStore.Count)
                        return true;
                    //else try this again as not everything was stored.
                }
                catch (Exception ex) {
                    finalException = ex;
                }
                currentTry++;
            } while (currentTry <= tries);
            throw new TimeoutException($"The chipcards couldn't be retrieved even after {tries} tries.", finalException);
        }
    }
}
