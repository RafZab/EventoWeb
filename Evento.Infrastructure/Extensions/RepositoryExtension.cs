using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<Ticket> GetTicketOrFailAsync(this IEventRepository eventRepository, Guid eventId, Guid ticketId)
        {
            var @event = await eventRepository.GetOrFailAsync(eventId);
            var ticket = @event.Tickets.SingleOrDefault(x => x.Id == ticketId);
            if (ticket == null)
                throw new Exception($"Ticket with id: '{ticketId}' was not found for event: '{@event.Name}'");
            return ticket;
        }
    }
}
