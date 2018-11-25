using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces {
    /// <summary>
    /// The implementation of this interface allows to get a chipcard by additional attributes.
    /// It useses tasks to allow for asynchronus operations.
    /// </summary>
    public interface IChipCardWhereGetter : IChipCardGetter {
        Task<HashSet<IChipCard>> LikeId(string ChipUId);
        Task<HashSet<IChipCard>> Within(DateTime fromIncluding, DateTime tillIncluding);
        Task<HashSet<IChipCard>> ByActive(bool isActive);
        Task<HashSet<IChipCard>> Where(Func<IChipCard, bool> where);
    }
}
