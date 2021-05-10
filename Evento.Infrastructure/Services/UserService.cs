using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(Guid userId, string emial, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(emial);
            if (user != null)
                throw new Exception($"User with email: '{emial}' already exists");
            user = new User(userId, role, name, emial, password);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string emial, string password)
        {
            var user = await _userRepository.GetAsync(emial);
            if (user != null)
                throw new Exception($"Invalid credentials");

            if(user.Password != password)
                throw new Exception($"Invalid credentials");

        }
    }
}
