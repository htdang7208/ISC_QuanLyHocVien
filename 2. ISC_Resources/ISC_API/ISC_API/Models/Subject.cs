using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("SUBJECTS")]
    public class Subject
    {
        [Key]
        public int SUBJECTID { get; set; }
        [MaxLength(200)]
        public string SUBJECTNAME { get; set; }
        public int NUMBERLESSON { get; set; }
        public bool STATUS { get; set; }
    }
}
