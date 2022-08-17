using BLL.Extensions;
using BLL.Interfaces;
using BLL.Validators.Cliente;
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
    //No mundo real, a camada de negócio não faz apenas validações, ela é responsável
    //por regras mais avançadas, log, cache, autorização
    //Install-Package FluentValidation
    public class ClienteService : IClienteService
    {
        private readonly IClienteDAO clienteDao;
        public ClienteService(IClienteDAO clienteDao)
        {
            this.clienteDao = clienteDao;
        }
        public async Task<DataResponse<Cliente>> GetAll()
        {
            return await clienteDao.GetAll();
        }
        //No mundo real, poderia estar sendo trabalhado a politica de cache do pet!
        public async Task<Response> Insert(Cliente c)
        {
            Response response = new ClienteInsertValidator().Validate(c).ConvertToResponse();
            //PetInsertValidator validator = new PetInsertValidator();
            //ValidationResult result = validator.Validate(p);
            //Response response = result.ConvertToResponse();

            if (!response.HasSuccess)
            //Se a validação não passou, retorne o response para tela!
            {
                return response;
            }
            //Se o pet está sendo cadastrado, então ele está ativo.
            c.EstaAtivo = true;
            //Se chegou aqui, é pq a validação passou e o PET está pronto pra ser cadastrado no banco.
            response = await clienteDao.Insert(c);
            return response;
        }
    }
}
