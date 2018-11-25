using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces {
    public interface IChipCardSetter {
        /// <summary>
        /// Inserts (Creates or Updates) the HashSet of chipcards.
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns>List of inserted Objects</returns>
        Task<HashSet<IChipCard>> Insert(HashSet<IChipCard> toUpdate);

        /// <summary>
        /// Deletes the chipcards within the Hashset.
        /// </summary>
        /// <param name="toDelete"></param>
        /// <returns></returns>
        Task<HashSet<IChipCard>> Delete(HashSet<IChipCard> toDelete);
    }
}
