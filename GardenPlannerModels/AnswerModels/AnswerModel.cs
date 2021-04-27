using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.AnswerModels
{
    public class AnswerModel
    {
        public string Answer { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Username { get; set; }
    }
}
