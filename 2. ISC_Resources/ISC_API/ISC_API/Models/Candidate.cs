using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("CANDIDATES")]
    public class Candidate
    {
        [Key]
        public int CANDIDATEID { get; set; }

        [MaxLength(50)]
        public string FIRSTNAME { get; set; }

        [MaxLength(50)]
        public string LASTNAME { get; set; }

        public bool GENDER { get; set; }

        public DateTime DOB { get; set; }

        [MaxLength(50)]
        public string EMAIL { get; set; }

        [MaxLength(15)]
        public string IDENTITYNUMBER { get; set; }
    }
}
