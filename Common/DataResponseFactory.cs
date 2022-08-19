using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataResponseFactory
    {
        public DataResponse<T> CreateSuccessDataResponse<T>(List<T> itens)
        {
            return new DataResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Data = itens,
            };
        }
        public DataResponse<T> CreateFailureDataResponse<T>(Exception ex)
        {
            return new DataResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex,
            };
        }
    }
}
