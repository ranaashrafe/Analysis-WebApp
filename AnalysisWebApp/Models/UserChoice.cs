using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnalysisWebApp.Models
{
    public class UserChoice
    {
        [Key]
        public int ID { get; set; }      
        public int UserID { get; set; }
  
        public bool IsFav { get; set; }
 
        public OrganDetail organDetail { get; set; }
        public int BasicOrganID { get; set; }
        public int OrganDetailID { get; set; }

    }
}
