﻿using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evento.Api.Controllers
{
    [Route("events/{eventId}/ticket")]
    [Authorize]
    public class TicketsController : ApiControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Get([FromRoute]Guid eventId, [FromRoute]Guid ticketId)
        {
            var ticket = await _ticketService.GetAsync(UserId, eventId, ticketId);
            if (ticket == null)
                return NotFound();
            return Json(ticket);
        }

        [HttpPost("purchase/{amount}")]
        public async Task<IActionResult> Post([FromRoute]Guid eventId, [FromRoute]int amount)
        {
            await _ticketService.PurchaseAsync(UserId, eventId, amount);
            return NoContent();
        }

        [HttpDelete("cancel/{amount}")]
        public async Task<IActionResult> Delete([FromRoute] Guid eventId, [FromRoute] int amount)
        {
            await _ticketService.CancelAsync(UserId, eventId, amount);
            return NoContent();
        }

    }
}
