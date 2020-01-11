using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models.Response
{
    public class CourseInfo
    {
        public int COURSEID { get; set; }
        public string COURSENAME { get; set; }
        public DateTime? STARTDATE { get; set; }
        public DateTime? ENDDATE { get; set; }
        public string NOTE { get; set; }
        public List<SpecializedTraining> listTrainings { get; set; }
    }
}
