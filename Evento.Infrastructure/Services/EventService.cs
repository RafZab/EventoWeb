using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<EventDto> GetAsync(Guid id)
        {
            var events = await _eventRepository.GetAsync(id);

            return _mapper.Map<EventDto>(events);
        }

        public async Task<EventDto> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);

            return _mapper.Map<EventDto>(@event);
        }

        public async Task<IEnumerable<EventDto>> BrowseAsync(string name = null)
        {
            var @event = await _eventRepository.BrowseAsync(name);
            
            return _mapper.Map<IEnumerable<EventDto>>(@event);
        }

        public async Task AddTicketAsync(Guid id, int amount, decimal price)
        {
            var @event = await _eventRepository.GetOrFailAsync(id);

            @event.AddTickets(amount, price);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task CreateAsync(Guid id, string name, string desscription, DateTime startDate, DateTime endDate)
        {
            var @event = await _eventRepository.GetOrFailAsync(name);

            @event = new Event(id, name, desscription, startDate, endDate);
            await _eventRepository.AddAsync(@event);
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var @event = await _eventRepository.GetOrFailAsync(name);

            @event = await _eventRepository.GetOrFailAsync(id);

            @event.SetName(name);
            @event.SetDescription(description);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task DeleteAsync(Guid id)
        {
            var @event = await _eventRepository.GetOrFailAsync(id);
            await _eventRepository.DeleteAsync(@event);
        }
    }
}
