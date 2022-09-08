using Common;
using DAO.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;


namespace DAO.Impl
{
    public class FuncionarioDAO : IFuncionarioDAO
    {
        private readonly ChamadosDbContext _db;
        public FuncionarioDAO(ChamadosDbContext db)
        {
            this._db = db;
        }
        public async Task<Response> Insert(Funcionario funcionario)
        {
            _db.Funcionarios.Add(funcionario);

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

        public async Task<Response> Update(Funcionario funcionario)
        {
            _db.Funcionarios.Update(funcionario);
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
        public async Task<Response> Delete(Funcionario funcionario)
        {
            _db.Funcionarios.Remove(funcionario);
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

        public async Task<DataResponse<Funcionario>> GetAll()
        {

            try
            {
                List<Funcionario> funcionarios = await _db.Funcionarios.ToListAsync();
                return DataResponseFactory<Funcionario>.CreateSuccessDataResponse(funcionarios);
            }
            catch (Exception ex)
            {
                return DataResponseFactory<Funcionario>.CreateFailureDataResponse(ex);
            }
        }

        public async Task<SingleResponse<Funcionario>> GetById(int id)
        {
            try
            {
                Funcionario item = await _db.Funcionarios.FindAsync(id);
                return SingleResponseFactory<Funcionario>.CreateSuccessSingleResponse(item);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<Funcionario>.CreateFailureSingleResponse(ex);
            }
        }

     
    }
}
