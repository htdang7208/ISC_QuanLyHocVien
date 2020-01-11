using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("UNIVERSITIES")]
    public class University
    {
        [Key]
        public int UNIVERSITYID { get; set; }
        [MaxLength(500)]
        public string UNIVERSITYNAME { get; set; }
    }
}
