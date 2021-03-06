using AutoMapper;
using Evento.Core.Domain;
using Evento.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evento.Infrastructure.Mappers
{
    public static class AutoMapperConfing
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {            // zmapować z , na 
                cfg.CreateMap<Event, EventDto>()
                    .ForMember(x => x.TicketCount, m => m.MapFrom(p => p.Tickets.Count()))
                    .ForMember(x => x.PurchasedTicketsCount, m => m.MapFrom(p => p.PurchasedTickets.Count()))
                    .ForMember(x => x.AvailableTicketsCount, m => m.MapFrom(p => p.AvailableTickets.Count()));
                cfg.CreateMap<Event, EventDetailsDto>();
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<Ticket, TicketDetailsDto>();
                cfg.CreateMap<User, AccountDto>();
            })
            .CreateMapper();
        

        
    }
}
