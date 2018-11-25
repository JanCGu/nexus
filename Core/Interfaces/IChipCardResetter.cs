using System.Threading.Tasks;

namespace Core.Interfaces {
    public interface IChipCardResetter {
        /// <summary>
        /// Allows to Reset the stored chipcard and prepares a new set.
        /// This operation may take a while.
        /// </summary>
        /// <returns></returns>
        Task<bool> Reset();
    }
}
