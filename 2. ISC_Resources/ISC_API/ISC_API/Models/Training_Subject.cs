using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("TRAINING_SUBJECTS")]
    public class Training_Subject
    {
        [Key]
        public int TRAINING_SUBJECT_ID { get; set; }
        public int SUBJECTID { get; set; }
        public int TRAININGID { get; set; }

        [ForeignKey("SUBJECTID")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("TRAININGID")]
        public virtual SpecializedTraining Training { get; set; }
    }
}
