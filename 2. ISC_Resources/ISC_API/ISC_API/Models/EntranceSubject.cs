using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("ENTRANCESUBJECTS")]
    public class EntranceSubject
    {
        [Key]
        public int ENTSUBJECTID { get; set; }

        [MaxLength(200)]
        public int ENTNAME { get; set; }
    }
}
