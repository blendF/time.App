using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }


        [Required]

        [Display(Name = "Role Name")]
        public string Name { get; set; }

    }
}
