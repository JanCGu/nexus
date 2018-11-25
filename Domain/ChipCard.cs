using System;

namespace Domain
{
    public class ChipCard : IChipCard
    {
        public DateTime ValidFrom { get; }
        public DateTime ValidTo { get; }
        public bool Active { get; }
        public string ChipUId { get; }

        public ChipCard(DateTime validFrom, DateTime validTo, bool active, string chipUId)
        {
            ValidFrom = validFrom;
            ValidTo = validTo;
            Active = active;
            ChipUId = string.IsNullOrEmpty(chipUId) ? throw new ArgumentNullException(nameof(chipUId)) : chipUId;
        }

        public ChipCard(IChipCard toConvert)
            : this(toConvert.ValidFrom, toConvert.ValidTo, toConvert.Active, toConvert.ChipUId) { }


        public override bool Equals(object obj)
        {
            if (ChipUId == null || obj == null)
                return false;
            if (!(obj is ChipCard toCompare))
                return false;
            return ChipUId.Equals(toCompare.ChipUId,StringComparison.CurrentCulture);
        }

        public override int GetHashCode()
        {
            return ChipUId.GetHashCode();
        }

        /// <summary>
        /// Returns a string representing this chipcard.
        /// It has the form:
        /// {ChipUId}\t:Valid:{ValidFrom} - {ValidTo},\tActive:{Active}
        /// </summary>
        public override string ToString()
        {
            return $"{ChipUId}\t:Valid:{ValidFrom} - {ValidTo},\tActive:{Active}";
        }
    }
}
