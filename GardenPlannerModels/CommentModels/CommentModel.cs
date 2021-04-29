using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerModels.CommentModels
{
    public class CommentModel
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
