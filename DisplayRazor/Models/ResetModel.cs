using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisplayRazor.Models {
    public class ResetModel {

        [Required]
        [Display(Name ="Maxium tries")]
        public int Tries { get; set; }
    }
}
