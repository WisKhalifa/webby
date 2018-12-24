using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webby.Models
{
    public class PostModels
    {
        public PostModels()
        {
            this.Comments = new HashSet<CommentModels>();
        }

        [Key]
        [ScaffoldColumn(false)]
        public int PostId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "Content")]
        public string PostContent { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<CommentModels> Comments { get; set; }

    }
}