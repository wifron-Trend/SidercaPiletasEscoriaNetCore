using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Alert
    {
        [Key]
        public int idAlert { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Boolean Content { get; set; }
        public Alert() { }
    }
}
