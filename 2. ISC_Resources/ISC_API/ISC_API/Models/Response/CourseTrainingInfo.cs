using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models.Response
{
    public class CourseTrainingInfo
    {
        public int CourseId { get; set; }
        public int TrainingId { get; set; }
        public string TrainingName { get; set; }
    }
}
