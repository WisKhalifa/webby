using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using webby.Interfaces;

namespace webby.Models
{
    public class PostRepository : IPostRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public void Add(PostModels p)
        {
            context.PostModels.Add(p);
            context.SaveChanges();
        }

        public void Remove(int Id)
        {
            PostModels pm = context.PostModels.Find(Id);
            context.PostModels.Remove(pm);
            context.SaveChanges();
        }

        public void Edit(PostModels p)
        {
            context.Entry(p).State = EntityState.Modified;
            context.SaveChanges();
        }

        public PostModels FindById(int Id)
        {
            var result = (from r in context.PostModels where r.PostId == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetPosts()
        {
            return context.PostModels;
        }
    }
}