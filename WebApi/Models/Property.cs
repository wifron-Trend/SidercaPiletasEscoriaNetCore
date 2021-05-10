using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Property", Schema = "Pool")]
    public partial class Property
    {
        public Property()
        {
            PoolAction = new HashSet<PoolAction>();
            PoolActionHistory = new HashSet<PoolActionHistory>();
            PoolStatusProperties = new HashSet<PoolStatusProperties>();
            StatusEmailType = new HashSet<StatusEmailType>();
        }

        [Key]
        [Column("idProperty")]
        public int IdProperty { get; set; }
        [Column("idDataType")]
        public int? IdDataType { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }

        [ForeignKey("IdDataType")]
        [InverseProperty("Property")]
        public DataType IdDataTypeNavigation { get; set; }
        [InverseProperty("IdPropertyNavigation")]
        public ICollection<PoolAction> PoolAction { get; set; }
        [InverseProperty("IdPropertyNavigation")]
        public ICollection<PoolActionHistory> PoolActionHistory { get; set; }
        [InverseProperty("IdPropertyNavigation")]
        public ICollection<PoolStatusProperties> PoolStatusProperties { get; set; }
        [InverseProperty("IdPropertyNavigation")]
        public ICollection<StatusEmailType> StatusEmailType { get; set; }
    }
}
