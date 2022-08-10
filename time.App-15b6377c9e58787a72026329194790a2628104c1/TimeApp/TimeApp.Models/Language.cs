using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string LanguageType { get; set; }
    }
}
