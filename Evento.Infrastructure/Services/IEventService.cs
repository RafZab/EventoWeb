using Evento.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public interface IEventService
    {
        Task<EventDto> GetAsync(Guid id);
        Task<EventDto> GetAsync(string name);
        Task<IEnumerable<EventDto>> BrowseAsync(string name = null);
        Task CreateAsync(Guid id, string name, string desscription,
            DateTime startDate, DateTime endDate);
        Task AddTicketAsync(Guid id, int amount, decimal price);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
    }
}
