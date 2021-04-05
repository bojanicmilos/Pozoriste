﻿using Pozoriste.Data.Entities;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Services
{
    public class PieceService : IPieceService
    {
        private readonly IPiecesRepository _pieceRepository;

        public PieceService(IPiecesRepository pieceRepository)
        {
            _pieceRepository = pieceRepository;
        }

        public async Task<PieceDomainModel> AddPiece(PieceDomainModel newPiece)
        {
            Piece pieceToCreate = new Piece()
            {
                Description = newPiece.Description,
                Genre = newPiece.Genre,
                IsActive = newPiece.isActive,
                Title = newPiece.Title,
                Year = newPiece.Year
            };

            Piece data = _pieceRepository.Insert(pieceToCreate);
            if(data == null)
            {
                return null;
            }

            _pieceRepository.Save();

            return new PieceDomainModel
            {
                Description = data.Description,
                Genre = data.Genre,
                Id = data.Id,
                isActive = data.IsActive,
                Title = data.Title,
                Year = data.Year
            };

        }

        public async Task<PieceDomainModel> DeletePiece(int Id)
        {
            Piece piece = await _pieceRepository.GetByIdAsync(Id);

            if(piece == null)
            {
                return null;
            }

            Piece data = await _pieceRepository.Delete(Id);

            if(data == null)
            {
                return null;
            }

            _pieceRepository.Save();

            return new PieceDomainModel
            {
                Id = data.Id,
                Description = data.Description,
                Genre = data.Genre,
                isActive = data.IsActive,
                Title = data.Title,
                Year = data.Year
            };
        }

        public async Task<IEnumerable<PieceDomainModel>> GetAllPieces()
        {
            var data = await _pieceRepository.GetAllAsync();

            if(data.Count() == 0)
            {
                return null;
            }

            List<PieceDomainModel> result = new List<PieceDomainModel>();

            foreach(var item in data)
            {
                PieceDomainModel piece = new PieceDomainModel()
                {
                    Description = item.Description,
                    Genre = item.Genre,
                    Id = item.Id,
                    isActive = item.IsActive,
                    Title = item.Title,
                    Year = item.Year
                };

                result.Add(piece);
            }

            return result;
        }

        public async Task<IEnumerable<PieceDomainModel>> GetAllPieces(bool? isActive)
        {
            var data = await _pieceRepository.GetActivePiecesAsync();

            if(data.Count() == 0)
            {
                return null;
            }

            List<PieceDomainModel> result = new List<PieceDomainModel>();

            foreach(var item in data)
            {
                PieceDomainModel piece = new PieceDomainModel
                {
                    Id = item.Id,
                    Description = item.Description,
                    Genre = item.Genre,
                    isActive = item.IsActive,
                    Title = item.Title,
                    Year = item.Year
                };

                result.Add(piece);
            }

            return result;
        }

        public async Task<PieceDomainModel> GetPieceByIdAsync(int Id)
        {
            Piece data = await _pieceRepository.GetByIdAsync(Id);

            if(data == null)
            {
                return null;
            }

            return new PieceDomainModel
            {
                Id = data.Id,
                Description = data.Description,
                Genre = data.Genre,
                isActive = data.IsActive,
                Title = data.Title,
                Year = data.Year
            };
        }

        public async Task<IEnumerable<PieceDomainModel>> GetTop10ByYearAsync(int year)
        {
            var data = await _pieceRepository.GetTop10ByYearAsync(year);

            if(data.Count() == 0)
            {
                return null;
            }

            List<PieceDomainModel> result = new List<PieceDomainModel>();

            foreach (var item in data)
            {
                PieceDomainModel piece = new PieceDomainModel()
                {
                    Description = item.Description,
                    Genre = item.Genre,
                    Id = item.Id,
                    isActive = item.IsActive,
                    Title = item.Title,
                    Year = item.Year
                };

                result.Add(piece);
            }

            return result;
        }
    }
}