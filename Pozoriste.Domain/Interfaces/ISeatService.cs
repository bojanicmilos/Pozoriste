using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface ISeatService
    {
        Task<SeatAuditoriumDomainModel> GetSeatsByAuditoriumId(int auditoriumId);
    }
}
