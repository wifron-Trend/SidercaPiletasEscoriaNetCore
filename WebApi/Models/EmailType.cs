using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("EmailType", Schema = "Pool")]
    public partial class EmailType
    {
        public EmailType()
        {
            StatusEmailType = new HashSet<StatusEmailType>();
        }

        [Key]
        [Column("idEmailType")]
        public long IdEmailType { get; set; }
        [StringLength(150)]
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public bool? Active { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }

        [InverseProperty("IdEmailTypeNavigation")]
        public ICollection<StatusEmailType> StatusEmailType { get; set; }
    }
}
