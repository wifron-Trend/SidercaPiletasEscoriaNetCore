using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Pool", Schema = "Pool")]
    public partial class Pool
    {
        public Pool()
        {
            PoolAction = new HashSet<PoolAction>();
            PoolActionHistory = new HashSet<PoolActionHistory>();
            PoolStatusEmail = new HashSet<PoolStatusEmail>();
            PoolStatusEmailHistory = new HashSet<PoolStatusEmailHistory>();
            PoolStatusProperties = new HashSet<PoolStatusProperties>();
        }

        [Key]
        [Column("idPool")]
        public int IdPool { get; set; }
        [StringLength(50)]
        public string Identification { get; set; }
        public bool? Active { get; set; }
        [Column("idStatus")]
        public int? IdStatus { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }
        public bool? SentEmail { get; set; }
        public DateTimeOffset? LastEmail { get; set; }
        public DateTimeOffset? DateTimeToAlarm { get; set; }

        [ForeignKey("IdStatus")]
        [InverseProperty("Pool")]
        public Status IdStatusNavigation { get; set; }
        [InverseProperty("IdPoolNavigation")]
        public ICollection<PoolAction> PoolAction { get; set; }
        [InverseProperty("IdPoolNavigation")]
        public ICollection<PoolActionHistory> PoolActionHistory { get; set; }
        [InverseProperty("IdPoolNavigation")]
        public ICollection<PoolStatusEmail> PoolStatusEmail { get; set; }
        [InverseProperty("IdPoolNavigation")]
        public ICollection<PoolStatusEmailHistory> PoolStatusEmailHistory { get; set; }
        [InverseProperty("IdPoolNavigation")]
        public ICollection<PoolStatusProperties> PoolStatusProperties { get; set; }
    }
}
