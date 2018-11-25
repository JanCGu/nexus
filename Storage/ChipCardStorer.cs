using Core.Interfaces;
using Core.Service;
using Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Storage {
    public class ChipCardStorer : IChipCardSetter {

        private readonly string storageLocation;
        private readonly IFormatter storageFormat;
        private readonly IChipCardDeleter storageDeleter;

        public ChipCardStorer(string StorageLocation, IFormatter storageFormat, IChipCardDeleter storageDeleter) {
            storageLocation = SettingsService.GetStorageLocationIfNullOrWhiteSpace(StorageLocation);
            this.storageFormat = storageFormat;
            this.storageDeleter = storageDeleter;
        }

        /// <summary>
        /// Regardless of the toDelete the whole set of stored chipcards will be deleted.
        /// 
        /// This implementation is due to time constraints and still fullfills the minimal
        /// requirements.
        /// </summary>
        /// <param name="toDelete"></param>
        /// <returns>The toDelete will be returned independet if it was in the storage or not.
        /// Only if the deletion failed a empty not null HashSet will be returned.</returns>
        public Task<HashSet<IChipCard>> Delete(HashSet<IChipCard> toDelete) {
            var ret = storageDeleter.DeleteAll() ? toDelete : new HashSet<IChipCard>();
            return Task.FromResult(ret);
        }

        /// <summary>
        /// This Implementation overwrites the stored objects.
        /// 
        /// This "inflexible" way of implementation is due to time constraints.
        /// A more flexible Implementation would allow to update or create seperate parts
        /// and not replace the whole storage object.
        /// It still fullfills the minimal requirement of storing the object and retriving it.
        /// The same holds for the sync instead of async implementation.
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <param name="stream">If it is null a FileStream is used to handle all insert operations. Otherwise the given stream will be used.</param>
        /// <returns>If the HashSet is empty the operation failed or the toUpdate was empty!</returns>
        public Task<HashSet<IChipCard>> Insert(HashSet<IChipCard> toUpdate, Stream stream) {
            if (!storageDeleter.DeleteAll())
                return Task.FromResult(new HashSet<IChipCard>());
            if (stream != null)
                storageFormat.Serialize(stream, toUpdate);
            else {
                using (stream = new FileStream(storageLocation,
                                                     FileMode.Create,
                                                     FileAccess.Write, FileShare.None)) {

                    storageFormat.Serialize(stream, MakeSerialisable(toUpdate));
                }
            }
            return Task.FromResult(toUpdate);
        }

        private HashSet<ChipCardDTO> MakeSerialisable(HashSet<IChipCard> toConvert) {
            return toConvert.Select(card => new ChipCardDTO(card)).ToHashSet();
        }

        public Task<HashSet<IChipCard>> Insert(HashSet<IChipCard> toUpdate) {
            return Insert(toUpdate, null);
        }
    }
}
