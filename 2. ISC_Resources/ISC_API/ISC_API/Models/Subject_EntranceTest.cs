using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("SUBJECT_ENTRANCETESTS")]
    public class Subject_EntranceTest
    {
        [Key]
        public int SUB_ENT_ID { get; set; }
        public int SUBJECTID { get; set; }
        public int ENTRANCETESTID { get; set; }
        public float PASSINGSCORE { get; set; }
        public DateTime STARTTIME { get; set; }
        public int LENGTH { get; set; }

        [ForeignKey("SUBJECTID")]
        public virtual EntranceSubject EntranceSubject { get; set; }

        [ForeignKey("ENTRANCETESTID")]
        public virtual EntranceTest EntranceTest { get; set; }
    }
}
