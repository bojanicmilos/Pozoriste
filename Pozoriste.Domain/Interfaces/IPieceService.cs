using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IPieceService
    {
        Task<IEnumerable<PieceDomainModel>> GetAllPieces();
        Task<IEnumerable<PieceDomainModel>> GetAllPieces(bool? isActive);
        Task<PieceDomainModel> GetPieceByIdAsync(int Id);
        Task<IEnumerable<PieceDomainModel>> GetTop10ByYearAsync(int year);
        Task<PieceDomainModel> AddPiece(PieceDomainModel newPiece);
        Task<PieceDomainModel> DeletePiece(int Id);
    }
}
