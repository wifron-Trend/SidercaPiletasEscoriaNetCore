using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("PoolStatusEmail", Schema = "Pool")]
    public partial class PoolStatusEmail
    {
        [Column("idPool")]
        public int IdPool { get; set; }
        [Column("idStatusEmailType")]
        public long IdStatusEmailType { get; set; }
        public DateTimeOffset? LastEmail { get; set; }
        public bool? SendEmail { get; set; }

        [ForeignKey("IdPool")]
        [InverseProperty("PoolStatusEmail")]
        public Pool IdPoolNavigation { get; set; }
        [ForeignKey("IdStatusEmailType")]
        [InverseProperty("PoolStatusEmail")]
        public StatusEmailType IdStatusEmailTypeNavigation { get; set; }
    }
}
