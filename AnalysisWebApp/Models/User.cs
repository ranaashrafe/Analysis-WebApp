using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalysisWebApp.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }          
        public string? Password { get; set; }
        public string? Image { get; set; }   
        public List<UserChoice> UserChoices { get; set; }
        public virtual User AddedByUser { get; set; }
       
    }
}
