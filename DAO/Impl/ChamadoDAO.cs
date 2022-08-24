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
        private readonly ChamadosDbContext _db;
        public ChamadoDAO(ChamadosDbContext db)
        {
            this._db = db;
        }

        public async Task<Response> Insert(Chamado chamado)
        {
            _db.Chamados.Add(chamado);
            try
            {
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }

        public async Task<Response> Update(Chamado chamados)
        {
            Chamado chamadoDB = await _db.Chamados.FindAsync(chamados.ID);
            chamadoDB.ID = chamados.ID;
            chamadoDB.Nome = chamados.Nome;
            chamadoDB.DescricaoCurta = chamados.DescricaoCurta;
            chamadoDB.DescricaoDetalhada = chamados.DescricaoDetalhada;
            chamadoDB.DataFim = chamados.DataFim;

            try
            {    
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }

        public async Task<Response> Delete(Cliente cliente)
        {
            _db.Clientes.Remove(cliente);

            try
            {
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponseWithEx(ex);
            }
        }
        //Terminar delete
        public async Task<DataResponse<Chamado>> GetAll()
        {
            try
            {
                List<Chamado> chamados = await _db.Chamados.ToListAsync();
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
                Chamado item = await _db.Chamados.FindAsync(id);
                if(id == null)
                {
                    return SingleResponseFactory<Chamado>.CreateFaiulureSingleResponse();
                }
                return SingleResponseFactory<Chamado>.CreateSuccessSingleResponse(item);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Chamado>.CreateFailureSingleResponse(ex);
            }
        }
    }
}
