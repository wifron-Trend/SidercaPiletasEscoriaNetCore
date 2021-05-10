using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("PoolActionHistory", Schema = "Pool")]
    public partial class PoolActionHistory
    {
        [Key]
        [Column("idPoolActionHistory")]
        public long IdPoolActionHistory { get; set; }
        [Column("idPool")]
        public int IdPool { get; set; }
        [StringLength(50)]
        public string Identification { get; set; }
        [Column("idStatus")]
        public int IdStatus { get; set; }
        [Column("idProperty")]
        public int IdProperty { get; set; }
        [Column("idUser")]
        public int? IdUser { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
        public DateTimeOffset? ValueDateTime { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }
        public bool? IsAlarm { get; set; }

        [ForeignKey("IdPool")]
        [InverseProperty("PoolActionHistory")]
        public Pool IdPoolNavigation { get; set; }
        [ForeignKey("IdProperty")]
        [InverseProperty("PoolActionHistory")]
        public Property IdPropertyNavigation { get; set; }
        [ForeignKey("IdStatus")]
        [InverseProperty("PoolActionHistory")]
        public Status IdStatusNavigation { get; set; }
        [ForeignKey("IdUser")]
        [InverseProperty("PoolActionHistory")]
        public User IdUserNavigation { get; set; }
    }
}
