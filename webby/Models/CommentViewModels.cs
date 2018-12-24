using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace webby.Models
{
    public class CommentViewModels
    {
        public int PostId { get; set; }

        public int CommentId { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

        public List<CommentModels> Comments { get; set; }

        //Get a view model of a comment from db
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