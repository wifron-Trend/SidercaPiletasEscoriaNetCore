using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("PoolStatusEmailHistory", Schema = "Pool")]
    public partial class PoolStatusEmailHistory
    {
        [Key]
        [Column("idPoolStatusEmailHistory")]
        public long IdPoolStatusEmailHistory { get; set; }
        [Column("idPool")]
        public int IdPool { get; set; }
        [Column("idStatusEmailType")]
        public long IdStatusEmailType { get; set; }
        public DateTimeOffset? LastEmail { get; set; }

        [ForeignKey("IdPool")]
        [InverseProperty("PoolStatusEmailHistory")]
        public Pool IdPoolNavigation { get; set; }
        [ForeignKey("IdStatusEmailType")]
        [InverseProperty("PoolStatusEmailHistory")]
        public StatusEmailType IdStatusEmailTypeNavigation { get; set; }
    }
}
