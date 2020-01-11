using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("TESTRESULTS")]
    public class TestResult
    {
        [Key]
        public int RESULTID { get; set; }
        public int ENTRANCETESTID { get; set; }
        public int CANDIDATEID { get; set; }
        public float SCORE { get; set; }
        public bool ISPASSING { get; set; }

        [ForeignKey("ENTRANCETESTID")]
        public virtual EntranceTest EntranceTest { get; set; }

        [ForeignKey("CANDIDATEID")]
        public virtual Candidate Cadidate { get; set; }
    }
}
