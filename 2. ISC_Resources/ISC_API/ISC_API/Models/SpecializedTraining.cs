using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("SPECIALIZEDTRAININGS")]
    public class SpecializedTraining
    {
        [Key]
        public int TRAININGID { get; set; }
        [MaxLength(500)]
        public string TRAININGNAME { get; set; }
        public Int16 NUMBERWEEK { get; set; }
        public bool STATUS { get; set; }
    }
}
