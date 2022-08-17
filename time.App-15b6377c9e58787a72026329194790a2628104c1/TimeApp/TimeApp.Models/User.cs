using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }


        public int Role_Id { get; set; }
        [ForeignKey("Role_Id")]
        public Role Role { get; set; }

        public int Language_Id { get; set; }
        [ForeignKey("Language_Id")]
        public Language Language { get; set; }
    }
}
