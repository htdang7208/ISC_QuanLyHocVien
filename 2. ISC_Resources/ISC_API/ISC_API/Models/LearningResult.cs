using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("LEARNINGRESULTS")]
    public class LearningResult
    {
        [Key]
        public int LEARNINGID { get; set; }
        public int CLASSID { get; set; }
        public int STUDENTID { get; set; }
        public float AVGSCORE { get; set; }
        public bool ATTENDFINALEXAM { get; set; }

        [ForeignKey("CLASSID")]
        public virtual Class Class { get; set; }

        [ForeignKey("STUDENTID")]
        public virtual Student Student { get; set; }
    }
}
