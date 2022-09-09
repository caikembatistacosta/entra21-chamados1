using BLL.Extensions;
using BLL.Interfaces;
using BLL.Validators.Chamados;
using Common;
using DAO.Impl;
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
        private readonly IChamadoDAO chamadoDAO;

        public ChamadoService(IChamadoDAO chamadoDAO)
        {
            this.chamadoDAO = chamadoDAO;
        }
        public async Task<DataResponse<Chamado>> GetAll()
        {
            return await chamadoDAO.GetAll();
        }

        public async Task<SingleResponse<Chamado>> GetById(int id)
        {
            return await chamadoDAO.GetById(id);
        }
        public async Task<Response> Delete(int id)
        {
            return await chamadoDAO.Delete(id);
        }

        public async Task<Response> Insert(Chamado chamado)
        {
            Response response = new ChamadoInsertValidator().Validate(chamado).ConvertToResponse();
            //PetInsertValidator validator = new PetInsertValidator();
            //ValidationResult result = validator.Validate(p);
            //Response response = result.ConvertToResponse();

            if (!response.HasSuccess)
            //Se a validação não passou, retorne o response para tela!
            {
                return response;
            }
            //Se o pet está sendo cadastrado, então ele está ativo.

            //Se chegou aqui, é pq a validação passou e o PET está pronto pra ser cadastrado no banco.
            response = await chamadoDAO.Insert(chamado);
            return response;
        }

        public async Task<Response> Update(Chamado chamado)
        {
            SingleResponse<Chamado> singleResponse = await chamadoDAO.GetById(chamado.ID);

            if (chamado == null)
            {
                return singleResponse;
            }

            Response response = new ChamadoUpdateValidator().Validate(chamado).ConvertToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }

            response = await chamadoDAO.Update(chamado);
            return response;
        }
    }
}
