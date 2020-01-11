using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int COURSEID { get; set; }

        [MaxLength(100)]
        public string COURSENAME { get; set; }

        public DateTime STARTDATE { get; set; }

        public DateTime ENDDATE { get; set; }

        [MaxLength(1200)]
        public string NOTE { get; set; }
    }
}
