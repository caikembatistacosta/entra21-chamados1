using Common;
using DAO.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Impl
{
    public class ChamadoDAO : IChamadoDAO
    {
        private readonly ChamadosDbContext _dbContext;
        public ChamadoDAO(ChamadosDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Response> Insert(Chamado chamado)
        {
            _dbContext.Chamados.Add(chamado);
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }

        public async Task<Response> Update(Chamado chamado)
        {
            _dbContext.Chamados.Update(chamado);
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }
        public async Task<DataResponse<Chamado>> GetAll()
        {
            try
            {
                List<Chamado> chamados = await _dbContext.Chamados.ToListAsync();
                return DataResponseFactory<Chamado>.CreateSuccessDataResponse(chamados);
            }
            catch (Exception ex)
            {
                return DataResponseFactory<Chamado>.CreateFailureDataResponse(ex);
            }
        }

        public async Task<SingleResponse<Chamado>> GetById(int id)
        {
            try
            {
                Chamado chamado = await _dbContext.Chamados.FindAsync(id);
                return SingleResponseFactory<Chamado>.CreateSuccessSingleResponse(chamado);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Chamado>.CreateFailureSingleResponse(ex);
            }
        }


    }
}
