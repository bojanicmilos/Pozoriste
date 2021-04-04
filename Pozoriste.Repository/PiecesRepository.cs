using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IPiecesRepository : IRepository<Piece>
    {

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

        public async Task<IEnumerable<Piece>> GetAll()
        {
            var data = await _theatreContext.Pieces.ToListAsync();

            return data;
        }

        public async Task<Piece> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Pieces.FindAsync(id);

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
