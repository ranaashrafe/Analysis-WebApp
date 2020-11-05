using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalysisWebApp.Models
{
    public class EditQuiz
    {
        public int userId { get; set; }
        public int organDetailId { get; set; }
        public bool IsFav { get; set; }

    }
}
