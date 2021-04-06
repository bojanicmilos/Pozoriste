﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pozoriste.API.Models;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PiecesController : ControllerBase
    {
        private readonly IPieceService _pieceService;

        public PiecesController(IPieceService pieceService)
        {
            _pieceService = pieceService;
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<CreatePieceDomainModel>> GetAsync(int id)
        {
            CreatePieceDomainModel piece;

            piece = await _pieceService.GetPieceByIdAsync(id);

            if(piece == null)
            {
                return NotFound(Messages.PIECE_DOES_NOT_EXIST);
            }

            return Ok(piece);
        }

        [HttpGet]
        [Route("active")]
        public async Task<ActionResult<IEnumerable<CreatePieceDomainModel>>> GetAsync()
        {
            IEnumerable<CreatePieceDomainModel> createPieceDomainModels;

            createPieceDomainModels = await _pieceService.GetAllPieces(true);

            if(createPieceDomainModels.Count() == 0)
            {
                createPieceDomainModels = new List<CreatePieceDomainModel>();
            }

            return Ok(createPieceDomainModels);
        }

        [HttpGet]
        [Route("top/{year}")]
        public async Task<ActionResult<IEnumerable<PieceDomainModel>>> GetTop10Async(int year)
        {
            IEnumerable<PieceDomainModel> pieceDomainModels;

            pieceDomainModels = await _pieceService.GetTop10ByYearAsync(year);

            if(pieceDomainModels.Count() == 0)
            {
                pieceDomainModels = new List<PieceDomainModel>();
            }

            return Ok(pieceDomainModels);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<CreatePieceDomainModel>>> GetAllAsync()
        {
            IEnumerable<CreatePieceDomainModel> createPieceDomainModels;

            createPieceDomainModels = await _pieceService.GetAllPieces();

            if(createPieceDomainModels.Count() == 0)
            {
                createPieceDomainModels = new List<CreatePieceDomainModel>();
            }

            return Ok(createPieceDomainModels);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Post([FromBody] PieceModel pieceModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PieceDomainModel domainModel = new PieceDomainModel
            {
                Description = pieceModel.Description,
                Genre = pieceModel.Genre,
                isActive = pieceModel.IsActive,
                Title = pieceModel.Title,
                Year = pieceModel.Year
            };

            PieceDomainModel createPiece;

            try
            {
                createPiece = await _pieceService.AddPiece(domainModel);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            if(createPiece == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.PIECE_CREATION_ERROR,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };

                return BadRequest(errorResponse);
            }

            return Created("pieces//" + createPiece.Id, createPiece);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            PieceDomainModel deletedPiece;
            try
            {
                deletedPiece = await _pieceService.DeletePiece(id);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            if(deletedPiece == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.PIECE_DOES_NOT_EXIST,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };

                return BadRequest(errorResponse);
            }

            return Accepted("pieces//" + deletedPiece.Id, deletedPiece);
        }
    }
}
