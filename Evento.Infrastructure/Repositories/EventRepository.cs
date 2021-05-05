﻿using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Repositories
{
    class EventRepository : IEventRepository
    {
        public static readonly ISet<Event> _events = new HashSet<Event>();
        public async Task<Event> GetAsync(Guid id)
        {
           return await Task.FromResult(_events.SingleOrDefault(x => x.Id == id));
        }

        public async Task<Event> GetAsync(string name)
            => await Task.FromResult(_events.SingleOrDefault(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant()));


        public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
        {
            var events = _events.AsEnumerable();
            if(!string.IsNullOrEmpty(name))
            {
                events = events.Where(x => x.Name.ToLowerInvariant()
                .Contains(name.ToLowerInvariant()));
            }
           return await Task.FromResult(events);
        }

        public async Task AddAsync(Event @event)
        {
            _events.Add(@event);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Event @event)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Event @event)
        {
            _events.Remove(@event);
            await Task.CompletedTask;
        }
    }
}
