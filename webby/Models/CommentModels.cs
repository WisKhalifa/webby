using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webby.Models
{
    public class CommentModels
    {
        public CommentModels()
        {
        }
        [Key]
        [ScaffoldColumn(false)]
        public int CommentId { get; private set; }
        [Required]
        public string Text { get; set; }
        
   
        
        public int PostId { get;  set; }

        public virtual PostModels Post { get; set; }

        public string Name { get; set; }
    }
}