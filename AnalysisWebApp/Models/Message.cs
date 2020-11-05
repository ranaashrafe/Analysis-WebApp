using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalysisWebApp.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public string MessageBody { get; set; }
        public DateTime DateTime { get; set; }
        public int OrganDetailID { get; set; }
       
     
    }
}
