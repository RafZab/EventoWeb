using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(Guid userId, string emial, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(emial);
            if (user != null)
                throw new Exception($"User with email: '{emial}' already exists");
            user = new User(userId, role, name, emial, password);
            await _userRepository.AddAsync(user);
        }

        public async Task<TokenDto> LoginAsync(string emial, string password)
        {
            var user = await _userRepository.GetAsync(emial);
            if (user != null)
                throw new Exception($"Invalid credentials");

            if(user.Password != password)
                throw new Exception($"Invalid credentials");

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDto
            {
                Token = jwt.Token,
                Role = user.Role,
                Expires = jwt.Expires
            };
        }
    }
}
