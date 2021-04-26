using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.QuestionModels
{
    public class GetQuestionsModel
    {
        public string Question { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
