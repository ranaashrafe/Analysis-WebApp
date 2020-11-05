using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalysisWebApp.Models
{
    public class OrganDetail
    {
        [Key]        
        public int ID { get; set; }
        public string Shape { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int OrganID { get; set; }
        public int MessageID { get; set; }
     
        public List<UserChoice> UserChoices { get; set; }
    }
}
