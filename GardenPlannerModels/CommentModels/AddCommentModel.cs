using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.CommentModels
{
    public class AddCommentModel
    {
        public int PlantID { get; set; }
        [MaxLength(50, ErrorMessage ="50 char limit")]
        public string Title { get; set; }
        [MaxLength(3000, ErrorMessage ="3000 char limit")]
        public string Comment { get; set; }
    }
}
