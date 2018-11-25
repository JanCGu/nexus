using Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace DisplayRazor.Models {
    /// <summary>
    /// This is an ACL implementation in order to be robust against future changes.
    /// </summary>
    public class ChipCardModel : IChipCard {
        [Display(Name = "Valid from")]
        public DateTime ValidFrom { get; set; }

        [Display(Name = "Valid to")]
        public DateTime ValidTo { get; set; }

        [Display(Name = "Is Active")]
        public bool Active { get; set; }

        [Display(Name = "Chipcard UId")]
        public string ChipUId { get; set; }

        public ChipCardModel(DateTime validFrom, DateTime validTo, bool active, string chipUId) {
            ValidFrom = validFrom;
            ValidTo = validTo;
            Active = active;
            ChipUId = chipUId ?? throw new ArgumentNullException(nameof(chipUId));
        }

        public ChipCardModel() { }

        public ChipCardModel(IChipCard toConvert) :
            this(toConvert.ValidFrom, toConvert.ValidTo, toConvert.Active, toConvert.ChipUId) { }
    }
}

