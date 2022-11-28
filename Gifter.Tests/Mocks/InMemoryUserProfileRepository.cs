﻿using Gifter.Models;
using Gifter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gifter.Tests.Mocks
{
    internal class InMemoryUserProfileRepository : IUserProfileRepository
    {
        private readonly List<UserProfile> _data;

        public List<UserProfile> InternalData
        {
            get
            {
                return _data;
            }
        }

        public InMemoryUserProfileRepository(List<UserProfile> startingData)
        {
            _data = startingData;
        }

        public void Add(UserProfile user)
        {
            var lastUser = _data.Last();
            user.Id = lastUser.Id + 1;
            _data.Add(user);
        }

        public void Delete(int id)
        {
            var userTodelete = _data.FirstOrDefault(p => p.Id == id);
            if (userTodelete == null)
            {
                return;
            }

            _data.Remove(userTodelete);
        }

        public List<UserProfile> GetAll()
        {
            return _data;
        }

        public UserProfile GetById(int id)
        {
            return _data.FirstOrDefault(p => p.Id == id);
        }

        public void Update(UserProfile user)
        {
            var currentUser = _data.FirstOrDefault(p => p.Id == user.Id);
            if (currentUser == null)
            {
                return;
            }

            currentUser.Name = user.Name;
            currentUser.Email = user.Email;
            currentUser.Bio = user.Bio;
            currentUser.ImageUrl = user.ImageUrl;
            currentUser.DateCreated = user.DateCreated;
        }
        public UserProfile GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public UserProfile GetByIdWithPosts (int id)
        {
            throw new NotImplementedException();
        }
    }
}
