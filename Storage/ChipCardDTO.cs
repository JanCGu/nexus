using System;
using Domain;

namespace Storage
{
    /// <summary>
    /// This is an ACL implementation in order to be robust against future changes.
    /// </summary>
    [Serializable]
    public class ChipCardDTO : IChipCard
    {
        public DateTime ValidFrom { get; }
        public DateTime ValidTo { get; }
        public bool Active { get; }
        public string ChipUId { get; }

        public ChipCardDTO(DateTime validFrom, DateTime validTo, bool active, string chipUId) {
            ValidFrom = validFrom;
            ValidTo = validTo;
            Active = active;
            ChipUId = string.IsNullOrEmpty(chipUId) ? throw new ArgumentNullException(nameof(chipUId)) : chipUId;
        }

        public ChipCardDTO(IChipCard toConvert)
            : this(toConvert.ValidFrom, toConvert.ValidTo, toConvert.Active, toConvert.ChipUId) { }


        public override bool Equals(object obj) {
            if (ChipUId == null || obj == null)
                return false;
            if (!(obj is ChipCardDTO toCompare))
                return false;
            return ChipUId.Equals(toCompare.ChipUId, StringComparison.CurrentCulture);
        }

        public override int GetHashCode() {
            return ChipUId.GetHashCode();
        }
    }
}
