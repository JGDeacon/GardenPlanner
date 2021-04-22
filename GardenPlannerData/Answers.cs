using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
    public class Answers
    {
        [Key]
        public int AnswerID { get; set; }
        [ForeignKey(nameof(Questions))]
        public int QuestionID { get; set; }
        public Questions Questions { get; set; }
        [Required, MaxLength(3000, ErrorMessage = "3000 Char Limit")]
        public string Answer { get; set; }        
        public Guid UserID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
