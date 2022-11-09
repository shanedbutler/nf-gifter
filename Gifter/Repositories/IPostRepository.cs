using Gifter.Models;
using System;
using System.Collections.Generic;

namespace Gifter.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Delete(int id);
        List<Post> GetAll();
        List<Post> GetAllWithComments();
        List<Post> Search(string q, bool sortDesc);
        List<Post> Latest(DateTime d, bool sortDesc);
        Post GetById(int id);
        Post GetByIdWithComments(int id);
        void Update(Post post);
    }
}
