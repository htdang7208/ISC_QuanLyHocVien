using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("STUDENTS")]
    public class Student
    {
        [Key]
        public int STUDENTID { get; set; }
        [MaxLength(50)]
        public string LASTNAME { get; set; }
        [MaxLength(50)]
        public string FIRSTNAME { get; set; }
        public bool GENDER { get; set; }
        [MaxLength(50)]
        public string EMAIL { get; set; }
        [MaxLength(12)]
        public string PHONENUMBER { get; set; }
        public DateTime DOB { get; set; }
        [MaxLength(15)]
        public string IDENTITYNUMBER { get; set; }
        [MaxLength(100)]
        public string  ADDRESS { get; set; }
        public bool CERTIFICATION { get; set; }
        public DateTime DATEREADYTOWORK { get; set; }
        public bool DEPOSITS { get; set; }
        public int UNIVERSITYID { get; set; }
        public int MAJORID { get; set; }

        [ForeignKey("UNIVERSITYID")]
        public virtual University University { get; set; }

        [ForeignKey("MAJORID")]
        public virtual Major Major { get; set; }
    }
}
