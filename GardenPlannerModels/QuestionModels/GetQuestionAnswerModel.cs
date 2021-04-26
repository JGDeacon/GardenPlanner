using GardenPlannerModels.AnswerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.QuestionModels
{
    public class GetQuestionAnswerModel
    {
        public string Question { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}
