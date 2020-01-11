using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models.Response
{
    public class StudentInfo
    {
        public int ID { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public bool GENDER { get; set; }
        public string EMAIL { get; set; }
        public string PHONENUMBER { get; set; }
        public DateTime DOB { get; set; }
        public string IDENTITYNUMBER { get; set; }
        public string ADDRESS { get; set; }
        public bool CERTIFICATION { get; set; }
        public DateTime DATEREADYTOWORK { get; set; }
        public bool DEPOSITS { get; set; }

        public virtual University University { get; set; }

        public virtual Major Major { get; set; }
    }
}
