using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IChipCard
    {
        DateTime ValidFrom { get; }
        DateTime ValidTo { get; }
        bool Active { get; }
        /// <summary>
        /// This identifier tells each chipcard apart from the next one.
        /// </summary>
        string ChipUId{get;}
    }
}
