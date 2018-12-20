using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace webby.Models
{
    public class PostListViewModels
    {
        public List<PostModels> Posts { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime PostTime { get; set; }
        public string PostContent { get; set; }
        public int AuthorId { get; set; }
        public virtual IEnumerable<CommentViewModels> Comments { get; set; }

        public static Expression<Func<PostModels, PostListViewModels>> ViewModel
        {
            get
            {
                return e => new PostListViewModels()
                {
                    PostId = e.PostId,
                    AuthorId = e.AuthorId,
                    Title = e.Title,
                    PostContent = e.PostContent,
                    Comments = e.Comments.AsQueryable().Select(CommentViewModels.ViewModel)
                };
            }
        }

    }
}