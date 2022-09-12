using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IChamadoDAO
    {
        Task<Response> Insert(Chamado chamado);
        Task<Response> Update(Chamado chamado);
        Task<DataResponse<Chamado>> GetAll();
        Task<SingleResponse<Chamado>> GetById(Chamado id);
    }
}
