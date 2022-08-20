using BLL.Interfaces;
using Common;
using DAO.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class ChamadoService : IChamadoService
    {
        private readonly IChamadoDAO _chamadoDAO;
        public ChamadoService(IChamadoDAO chamadoDAO)
        {
            _chamadoDAO = chamadoDAO;
        }
        public async Task<Response> Insert(Chamado chamado)
        {
            return await _chamadoDAO.Insert(chamado);
        }

        public async Task<Response> Update(Chamado chamado)
        {
            return await _chamadoDAO.Update(chamado);
        }
        public async Task<DataResponse<Chamado>> GetAll()
        {
            return await _chamadoDAO.GetAll();
        }

        public async Task<SingleResponse<Chamado>> GetById(int id)
        {
            return await _chamadoDAO.GetById(id);
        }

      
    }
}
