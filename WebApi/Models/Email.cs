using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Email", Schema = "Pool")]
    public partial class Email
    {
        [Key]
        [Column("idEmail")]
        public int IdEmail { get; set; }
        [Required]
        [StringLength(150)]
        public string EmailAddress { get; set; }
        public bool? Active { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }
    }
}
