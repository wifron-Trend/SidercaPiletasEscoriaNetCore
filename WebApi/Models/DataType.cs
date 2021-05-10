using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("DataType", Schema = "Pool")]
    public partial class DataType
    {
        public DataType()
        {
            Property = new HashSet<Property>();
        }

        [Key]
        [Column("idDataType")]
        public int IdDataType { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Type { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset? InsDateTime { get; set; }

        [InverseProperty("IdDataTypeNavigation")]
        public ICollection<Property> Property { get; set; }
    }
}
