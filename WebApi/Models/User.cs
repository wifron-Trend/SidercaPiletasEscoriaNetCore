using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("User", Schema = "Pool")]
    public partial class User
    {
        public User()
        {
            PoolAction = new HashSet<PoolAction>();
            PoolActionHistory = new HashSet<PoolActionHistory>();
        }

        [Key]
        [Column("idUser")]
        public int IdUser { get; set; }
        [StringLength(50)]
        public string Identification { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public bool? Active { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }
        public bool? IsAdmin { get; set; }

        [InverseProperty("IdUserNavigation")]
        public ICollection<PoolAction> PoolAction { get; set; }
        [InverseProperty("IdUserNavigation")]
        public ICollection<PoolActionHistory> PoolActionHistory { get; set; }
    }
}
