using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("PoolAction", Schema = "Pool")]
    public partial class PoolAction
    {
        [Column("idPool")]
        public int IdPool { get; set; }
        [Column("idStatus")]
        public int IdStatus { get; set; }
        [Column("idProperty")]
        public int IdProperty { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
        public DateTimeOffset? ValueDateTime { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset? InsDateTime { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset? UpdDateTime { get; set; }
        [Column("idUser")]
        public int? IdUser { get; set; }

        [ForeignKey("IdPool")]
        [InverseProperty("PoolAction")]
        public Pool IdPoolNavigation { get; set; }
        [ForeignKey("IdProperty")]
        [InverseProperty("PoolAction")]
        public Property IdPropertyNavigation { get; set; }
        [ForeignKey("IdStatus")]
        [InverseProperty("PoolAction")]
        public Status IdStatusNavigation { get; set; }
        [ForeignKey("IdUser")]
        [InverseProperty("PoolAction")]
        public User IdUserNavigation { get; set; }
    }
}
