using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.CommentModels
{
    public class GetCommentsModel
    {
        public int PlantID { get; set; }
        public string PlantName { get; set; }
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public int Likes { get; set; }
    }
}
