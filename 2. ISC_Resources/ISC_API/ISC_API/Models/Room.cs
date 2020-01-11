using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("ROOMS")]
    public class Room
    {
        [Key]
        public int ROOMID { get; set; }
        [MaxLength(100)]
        public string ROOMNAME { get; set; }
        public int CAPACITY { get; set; }
    }
}
