using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace webby.Models
{
    public class CommentViewModels
    {
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public static Expression<Func<CommentModels, CommentViewModels>> ViewModel
        {
            get
            {
                return c => new CommentViewModels()
                {
                    Text = c.Text,
                    AuthorId = c.AuthorId
                };
            }
        }

    }
}