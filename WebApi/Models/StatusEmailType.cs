using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("StatusEmailType", Schema = "Pool")]
    public partial class StatusEmailType
    {
        public StatusEmailType()
        {
            PoolStatusEmail = new HashSet<PoolStatusEmail>();
            PoolStatusEmailHistory = new HashSet<PoolStatusEmailHistory>();
        }

        [Key]
        [Column("idStatusEmailType")]
        public long IdStatusEmailType { get; set; }
        [Column("idStatus")]
        public int? IdStatus { get; set; }
        [Column("idProperty")]
        public int? IdProperty { get; set; }
        [Column("idEmailType")]
        public long? IdEmailType { get; set; }
        [Column("interval")]
        public TimeSpan? Interval { get; set; }
        [Column("idStatusNextPool")]
        public int? IdStatusNextPool { get; set; }

        [ForeignKey("IdEmailType")]
        [InverseProperty("StatusEmailType")]
        public EmailType IdEmailTypeNavigation { get; set; }
        [ForeignKey("IdProperty")]
        [InverseProperty("StatusEmailType")]
        public Property IdPropertyNavigation { get; set; }
        [ForeignKey("IdStatus")]
        [InverseProperty("StatusEmailTypeIdStatusNavigation")]
        public Status IdStatusNavigation { get; set; }
        [ForeignKey("IdStatusNextPool")]
        [InverseProperty("StatusEmailTypeIdStatusNextPoolNavigation")]
        public Status IdStatusNextPoolNavigation { get; set; }
        [InverseProperty("IdStatusEmailTypeNavigation")]
        public ICollection<PoolStatusEmail> PoolStatusEmail { get; set; }
        [InverseProperty("IdStatusEmailTypeNavigation")]
        public ICollection<PoolStatusEmailHistory> PoolStatusEmailHistory { get; set; }
    }
}
