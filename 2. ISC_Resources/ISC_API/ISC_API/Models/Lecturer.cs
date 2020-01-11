using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("LECTURERS")]
    public class Lecturer
    {
        [Key]
        public int LECTURERID { get; set; }
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
        [MaxLength(200)]
        public string DEGREE { get; set; }
        [MaxLength(200)]
        public string ACADEMICRANK { get; set; }
        public DateTime STARTDATE { get; set; }
    }
}
