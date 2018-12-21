using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

  
        public string PostContent { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<CommentModels> Comments { get; set;
    }
    }
}