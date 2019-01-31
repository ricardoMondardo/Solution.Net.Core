﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.Models.User;

namespace Web.Api.Services.Interface
{
    public interface IUserService
    {
        void Add(User user);
        bool IsEmailUniq(string email);
        bool IsUsernameUniq(string username);

        User GetSingle(string email);
    }
}
