using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces {
    public interface IChipCardGetter {
        /// <summary>
        /// Gets all chipcards.
        /// </summary>
        /// <returns></returns>
        Task<HashSet<IChipCard>> All();
    }
}
