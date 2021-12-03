using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("AppUsers")]
    public class AppUsers
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] UserPasswordHash { get; set; }

        public byte[] UserPasswordSalt { get; set; }
    }
}