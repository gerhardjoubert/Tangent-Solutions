using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdNumbers.Api.Models
{
    public class IdNumberModel
    {
        [Required(ErrorMessage = "idNumber is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "idNumber should have a length of 13")]
        public string idNumber { get; set; }
    }
}
