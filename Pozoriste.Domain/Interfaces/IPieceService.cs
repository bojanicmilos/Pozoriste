using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IPieceService
    {
        Task<IEnumerable<CreatePieceDomainModel>> GetAllPieces();
        Task<IEnumerable<CreatePieceDomainModel>> GetAllPieces(bool? isActive);
        Task<CreatePieceDomainModel> GetPieceByIdAsync(int Id);
        Task<IEnumerable<PieceDomainModel>> GetTop10ByYearAsync(int year);
        Task<PieceDomainModel> AddPiece(PieceDomainModel newPiece);
        Task<PieceDomainModel> DeletePiece(int Id);
    }
}
