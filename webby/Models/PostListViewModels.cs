using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webby.Models
{
    public class PostListViewModels
    {
        public List<PostModels> Posts { get; set; }
        public string Title { get; set; }
        public DateTime PostTime { get; set; }
        public string PostContent { get; set; }
        public virtual ICollection<CommentModels> Comments { get; set; }

    }
}