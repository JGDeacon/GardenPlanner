using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerData
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }
        [ForeignKey(nameof(Plants))]
        public int PlantID { get; set; }
        public Plants Plants { get; set; }
        [Required, MaxLength(50, ErrorMessage = "50 Char Limit")]
        public string Title { get; set; }
        [Required, MaxLength(3000, ErrorMessage = "3000 Char Limit")]
        public string Comment { get; set; }
        public Guid UserID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
