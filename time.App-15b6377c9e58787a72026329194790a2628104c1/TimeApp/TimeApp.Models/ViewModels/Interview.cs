using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Models.ViewModels
{
   public class Interview
    {
        [Key]
        public int  Id{ get; set; }
        public int Interviews { get; set;}
        public int User_Id { get; set; }
        [ForeignKey("User_Id")]
        public User User { get; set; }

        public DateTime Data { get; set; }

    }
}
