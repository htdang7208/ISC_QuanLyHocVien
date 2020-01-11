using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("COURSE_TRAININGS")]
    public class Course_Training
    {
        [Key]
        public int COURSE_TRAINING_ID { get; set; }
        public int COURSEID { get; set; }
        public int TRAININGID { get; set; }

        [ForeignKey("COURSEID")]
        public virtual Course  Course { get; set; }

        [ForeignKey("TRAININGID")]
        public virtual SpecializedTraining Training { get; set; }
    }
}
