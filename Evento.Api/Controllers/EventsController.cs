using Evento.Infrastructure.Commands.Events;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evento.Api.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get([FromRoute]Guid eventId)
        {
            var @event = await _eventService.GetAsync(eventId);
            if (@event == null)
                return NotFound();
            return Json(@event);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var events = await _eventService.BrowseAsync(name);

            return Json(events);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEvent commad)
        {
            commad.EventId = Guid.NewGuid();
            await _eventService.CreateAsync(commad.EventId, commad.Name, commad.Description, commad.StartDate, commad.EndDate);
            await _eventService.AddTicketAsync(commad.EventId, commad.Tickets, commad.Price);

            return Created($"/events/{commad.EventId}", null);
        }
        
        [HttpPut("{eventId}")]
        public async Task<IActionResult> Post([FromRoute]Guid eventId, [FromBody] UpdateEvent commad)
        {
            commad.EventId = eventId;
            await _eventService.UpdateAsync(commad.EventId, commad.Name, commad.Description);

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete([FromRoute]Guid eventId)
        {
            await _eventService.DeleteAsync(eventId);

            return NoContent();
        }

    }
}
