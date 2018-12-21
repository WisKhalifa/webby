using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace webby.Models
{
    public class CommentViewModels
    {
        public int PostId { get; set; }
        public int CommentId { get;  set; }

        public string Text { get; set; }
        public string Name { get; set; }
        public List<CommentModels> Comments { get; set; }
        public static Expression<Func<CommentModels, CommentViewModels>> ViewModel
        {
            get
            {
                return c => new CommentViewModels()
                {
                    Name = c.Name,
                    PostId = c.PostId,
                    Text = c.Text,
                };
            }
        }

    }
}