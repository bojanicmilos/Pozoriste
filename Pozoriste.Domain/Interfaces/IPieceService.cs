using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IPieceService
    {
        Task<IEnumerable<PieceDomainModel1>> GetAllPieces();
        Task<IEnumerable<PieceDomainModel1>> GetAllPieces(bool? isActive);
        Task<PieceDomainModel1> GetPieceByIdAsync(int Id);
        Task<IEnumerable<PieceDomainModel>> GetTop10ByYearAsync(int year);
        Task<PieceDomainModel> AddPiece(PieceDomainModel newPiece);
        Task<PieceDomainModel> DeletePiece(int Id);
    }
}
