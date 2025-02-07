﻿using Common;
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
    public class DemandaDAO : IDemandaDAO
    {
        private readonly DemandasDbContext _db;
        public DemandaDAO(DemandasDbContext db)
        {
            this._db = db;
        }

        public async Task<Response> Insert(Demanda Demanda)
        {
            _db.Demandas.Add(Demanda);
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

        public async Task<Response> Update(Demanda Demandas)
        {
            Demanda DemandaDB = await _db.Demandas.FindAsync(Demandas.ID);
            DemandaDB.ID = Demandas.ID;
            DemandaDB.Nome = Demandas.Nome;
            DemandaDB.DescricaoCurta = Demandas.DescricaoCurta;
            DemandaDB.DescricaoDetalhada = Demandas.DescricaoDetalhada;
            DemandaDB.DataFim = Demandas.DataFim;

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
        public async Task<Response> UpdateStatus(Demanda Demandas)
        {
            Demanda DemandaDB = await _db.Demandas.FindAsync(Demandas.ID);
            DemandaDB.ID = Demandas.ID;
            DemandaDB.StatusDaDemanda = Demandas.StatusDaDemanda;

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
        public async Task<DataResponse<Demanda>> GetAll()
        {
            try
            {
                List<Demanda> Demandas = await _db.Demandas.OrderByDescending(c=> c.ID).ToListAsync();
                return DataResponseFactory<Demanda>.CreateSuccessDataResponse(Demandas);

            }
            catch (Exception ex)
            {
                return DataResponseFactory<Demanda>.CreateFailureDataResponse(ex);
            }
        }
        public async Task<DataResponse<Demanda>> GetLast6()
        {
            try
            {
                List<Demanda> Demandas = await _db.Demandas.OrderByDescending(c => c.ID).Take(6).ToListAsync();
                return DataResponseFactory<Demanda>.CreateSuccessDataResponse(Demandas);

            }
            catch (Exception ex)
            {
                return DataResponseFactory<Demanda>.CreateFailureDataResponse(ex);
            }
        }
        public async Task<SingleResponse<Demanda>> GetById(int id)
        {
            try
            {
                Demanda item = await _db.Demandas.FindAsync(id);
                if(id == null)
                {
                    return SingleResponseFactory<Demanda>.CreateFaiulureSingleResponse();
                }
                return SingleResponseFactory<Demanda>.CreateSuccessSingleResponse(item);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Demanda>.CreateFailureSingleResponse(ex);
            }
        }
    }
}
