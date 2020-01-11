using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("CLASSES")]
    public class Class
    {
        [Key]
        public int CLASSID { get; set; }

        [MaxLength(200)]
        public string CLASSNAME { get; set; }

        public float OBSENTALLOW { get; set; }

        public float PASSINGSCORE { get; set; }

        public int SUBJECTID { get; set; }

        public int LECTURERID { get; set; }

        public int COURSEID { get; set; }

        [ForeignKey("SUBJECTID")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("LECTURERID")]
        public virtual Lecturer Lecturer { get; set; }

        [ForeignKey("COURSEID")]
        public virtual Course Course { get; set; }
    }
}
