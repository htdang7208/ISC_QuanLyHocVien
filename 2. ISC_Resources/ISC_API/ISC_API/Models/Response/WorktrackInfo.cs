using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models.Response
{
    public class WorktrackInfo
    {
        public int WORKTRACKID { get; set; }
        public DateTime STARTDATE { get; set; }
        public Nullable<System.DateTime> CONTRACTDATE { get; set; }
        public byte STATUS { get; set; }
        public string NOTE { get; set; }
        public virtual Company Company { get; set; }
        public virtual Student Student { get; set; }
    }
}
