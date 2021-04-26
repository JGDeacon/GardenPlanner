using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
    public class Likes
    {
        [Key]
        public int LikeID { get; set; }
        public bool IsLiked { get; set; }
        [ForeignKey(nameof(Plants))]
        public int PlantID { get; set; }
        public Plants Plants { get; set; }
        public Guid UserID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
