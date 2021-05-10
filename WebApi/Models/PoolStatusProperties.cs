using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("PoolStatusProperties", Schema = "Pool")]
    public partial class PoolStatusProperties
    {
        [Key]
        [Column("idPoolStatusProperties")]
        public int IdPoolStatusProperties { get; set; }
        [Column("idStatus")]
        public int IdStatus { get; set; }
        [Column("idPool")]
        public int IdPool { get; set; }
        [Column("idProperty")]
        public int IdProperty { get; set; }
        public bool? Active { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }
        public bool? IsCountDown { get; set; }

        [ForeignKey("IdPool")]
        [InverseProperty("PoolStatusProperties")]
        public Pool IdPoolNavigation { get; set; }
        [ForeignKey("IdProperty")]
        [InverseProperty("PoolStatusProperties")]
        public Property IdPropertyNavigation { get; set; }
        [ForeignKey("IdStatus")]
        [InverseProperty("PoolStatusProperties")]
        public Status IdStatusNavigation { get; set; }
    }
}
