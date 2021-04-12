using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IPiecesRepository : IRepository<Piece>
    {
        Task<IEnumerable<Piece>> GetActivePiecesAsync();
        Task<IEnumerable<Piece>> GetTop10ByYearAsync(int year);
    }
    public class PiecesRepository : IPiecesRepository
    {
        private TheatreContext _theatreContext;

        public PiecesRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Piece> Delete(int id)
        {
            Piece existing = await _theatreContext.Pieces.FindAsync(id);
            var result = _theatreContext.Pieces.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Piece>> GetActivePiecesAsync()
        {
            var data = await _theatreContext.Pieces.Where(x => x.IsActive).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<Piece>> GetAllAsync()
        {
            var data = await _theatreContext.Pieces.ToListAsync();

            return data;
        }

        public async Task<Piece> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Pieces
                .Include(x => x.Shows)
                .FirstOrDefaultAsync(x => x.Id == id);

            return data;
        }

        public async Task<IEnumerable<Piece>> GetTop10ByYearAsync(int year)
        {
            var data = await _theatreContext.Pieces.Where(x => x.Year == year).Take(10).ToListAsync();

            return data;
        }

        public Piece Insert(Piece obj)
        {
            return _theatreContext.Pieces.Add(obj).Entity;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }

        public Piece Update(Piece obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }
    }
}
