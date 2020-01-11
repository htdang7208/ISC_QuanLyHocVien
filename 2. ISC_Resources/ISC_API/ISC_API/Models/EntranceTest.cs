using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("ENTRACETESTS")]
    public class EntranceTest
    {
        [Key]
        public int ENTRANCETESTID { get; set; }
        public int COURSEID { get; set; }
        public DateTime TESTDATE { get; set; }
        [MaxLength(200)]
        public string TESTNAME { get; set; }
        
        [ForeignKey("COURSEID")]
        public virtual Course Course { get; set; }
    }
}
