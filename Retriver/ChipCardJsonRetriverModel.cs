using Domain;
using Newtonsoft.Json;
using System;

namespace Retriver
{
    /// <summary>
    /// This is an ACL implementation of the ChipCard in order to be robust against future chainges.
    /// </summary>
    public class ChipCardJsonRetriverModel : ChipCard
    {
        [JsonConstructor]
        public ChipCardJsonRetriverModel(DateTime validFrom, DateTime validTo, bool active, string chipUId)
            : base(validFrom, validTo, active, chipUId) { }

        public ChipCardJsonRetriverModel(IChipCard toConvert)
            :base(toConvert) { }
    }
}
