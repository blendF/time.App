using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models.ViewModels
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display (Name = "Shto Gjuhen")]
        public string Gjuha { get; set; }

    }
}
