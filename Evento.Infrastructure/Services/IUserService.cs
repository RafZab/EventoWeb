﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userId, string emial,
            string name, string password, string role = "user");
        Task LoginAsync(string emial, string password);
    }
}
