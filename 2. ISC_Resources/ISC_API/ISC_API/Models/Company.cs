using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("COMPANIES")]
    public class Company
    {
        [Key]
        public int COMPANYID { get; set; }

        [MaxLength(200)]
        public string COMPANYNAME { get; set; }

        [MaxLength(200)]
        public string CONTACTPERSON { get; set; }

        [MaxLength(100)]
        public string EMAIL { get; set; }

        [MaxLength(100)]
        public string ADDRESS { get; set; }

        [MaxLength(12)]
        public string PHONENUMBER { get; set; }

        public bool STATUS { get; set; }
    }
}
