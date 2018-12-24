using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webby.Models;

namespace webby.Interfaces
{
    public interface IPostRepository
    {

        void Add(PostModels p);
        void Edit(PostModels p);
        void Remove(int Id);
        IEnumerable GetPosts();
        PostModels FindById(int Id);
        
    }
}
