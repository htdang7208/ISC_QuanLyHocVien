using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models.Response
{
    public class TrainingInfo
    {
        public int TRAININGID { get; set; }
        public string TRAININGNAME { get; set; }
        public Int16? NUMBERWEEK { get; set; }
        public bool STATUS { get; set; }
        public List<Subject> listSubjects { get; set; }
    }
}
