using Core.Interfaces;
using Core.Service;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Storage {
    public class ChipCardRetriver : IChipCardWhereGetter {

        private readonly string storageLocation;
        private readonly IFormatter storageFormatter;

        public ChipCardRetriver(IFormatter storageFormatter, string storageLocation) {
            this.storageFormatter = storageFormatter;
            this.storageLocation = SettingsService.GetStorageLocationIfNullOrWhiteSpace(storageLocation);
        }

        /// <summary>
        /// In principal this should be an async implementation.
        /// 
        /// Due to not needed requirements and time constraints this is only a serial implementaiton.
        /// </summary>
        /// <param name="stream">Uses the stream for the storage operation. If it is null a FileStream is used.</param>
        /// <returns></returns>
        public Task<HashSet<IChipCard>> All(Stream stream) {
            if (stream != null)
                return HandleStreamForAll(stream);

            using (stream = new FileStream(storageLocation,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read)) {
                return HandleStreamForAll(stream);
            }
        }

        private Task<HashSet<IChipCard>> HandleStreamForAll(Stream stream) {
            //the ChipCardStorer has to ensure that a HashSet<ChipCardDTO> is stored and not something else.
            var ret = (HashSet<ChipCardDTO>)storageFormatter.Deserialize(stream);
            return Task.FromResult(ret.Select(card => card as IChipCard).ToHashSet());
        }

        public Task<HashSet<IChipCard>> ByActive(bool isActive, Stream stream) {

            return Where(chipcard => chipcard.Active == isActive, stream);
        }

        /// <summary>
        /// Uses Contains and therefore the position of the ChipUId within the
        /// ChipCard.ChipUId does not realy metter as long as it is a part.
        /// </summary>
        /// <param name="stream">Uses the stream for the storage operation. If it is null a FileStream is used.</param>
        public Task<HashSet<IChipCard>> LikeId(string ChipUId, Stream stream) {
            return Where(chipcard => chipcard.ChipUId.Contains(ChipUId), stream);
        }

        public async Task<HashSet<IChipCard>> Where(Func<IChipCard, bool> where, Stream stream) {
            var chipcards = await All(stream);
            return chipcards.Where(chipcard => where(chipcard)).ToHashSet();
        }

        public Task<HashSet<IChipCard>> Within(DateTime fromIncluding, DateTime tillIncluding, Stream stream) {
            return Where(chipcard => chipcard.ValidFrom >= fromIncluding && chipcard.ValidTo <= tillIncluding, stream);
        }

        public Task<HashSet<IChipCard>> LikeId(string ChipUId) {
            return LikeId(ChipUId, null);
        }

        public Task<HashSet<IChipCard>> Within(DateTime fromIncluding, DateTime tillIncluding) {
            return Within(fromIncluding, tillIncluding, null);
        }

        public Task<HashSet<IChipCard>> ByActive(bool isActive) {
            return ByActive(isActive, null);
        }

        public Task<HashSet<IChipCard>> Where(Func<IChipCard, bool> where) {
            return Where(where, null);
        }

        public Task<HashSet<IChipCard>> All() {
            return All(null);
        }
    }
}
