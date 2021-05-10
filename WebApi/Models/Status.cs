using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Status", Schema = "Pool")]
    public partial class Status
    {
        public Status()
        {
            Pool = new HashSet<Pool>();
            PoolAction = new HashSet<PoolAction>();
            PoolActionHistory = new HashSet<PoolActionHistory>();
            PoolStatusProperties = new HashSet<PoolStatusProperties>();
            StatusEmailTypeIdStatusNavigation = new HashSet<StatusEmailType>();
            StatusEmailTypeIdStatusNextPoolNavigation = new HashSet<StatusEmailType>();
        }

        [Key]
        [Column("idStatus")]
        public int IdStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool? EmailAlert { get; set; }
        [Required]
        public bool? Active { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset? InsDateTime { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset? UpdDateTime { get; set; }
        [StringLength(255)]
        public string Color { get; set; }

        [InverseProperty("IdStatusNavigation")]
        public ICollection<Pool> Pool { get; set; }
        [InverseProperty("IdStatusNavigation")]
        public ICollection<PoolAction> PoolAction { get; set; }
        [InverseProperty("IdStatusNavigation")]
        public ICollection<PoolActionHistory> PoolActionHistory { get; set; }
        [InverseProperty("IdStatusNavigation")]
        public ICollection<PoolStatusProperties> PoolStatusProperties { get; set; }
        [InverseProperty("IdStatusNavigation")]
        public ICollection<StatusEmailType> StatusEmailTypeIdStatusNavigation { get; set; }
        [InverseProperty("IdStatusNextPoolNavigation")]
        public ICollection<StatusEmailType> StatusEmailTypeIdStatusNextPoolNavigation { get; set; }
    }
}
