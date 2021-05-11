using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static async Task<Event> GetOrFailAsync(this IEventRepository eventRepository,Guid id)
        {
            var @event = await eventRepository.GetAsync(id);
            if (@event == null)
                throw new Exception($"Event with id: '{id}' doesn't exists");
            return @event;
        }

        public static async Task<Event> GetOrFailAsync(this IEventRepository eventRepository, string name)
        {
            var @event = await eventRepository.GetAsync(name);
            if (@event != null)
                throw new Exception($"Event named: '{name}' already exists");
            return @event;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null)
                throw new Exception($"Event with id: '{id}' doesn't exists");
            return user;
        }

    }
}
