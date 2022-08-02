using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models.ViewModels
{
     public class Role
    {
        [Key]
        public  int Id { get; set; }

        [Required]

        [Display(Name="Role Name")]
        public  string Name{ get; set; }

    }
}
