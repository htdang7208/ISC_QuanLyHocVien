using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("MAJORS")]
    public class Major
    {
        [Key]
        public int MAJORID { get; set; }
        [MaxLength(200)]
        public string MAJORNAME { get; set; }
    }
}
