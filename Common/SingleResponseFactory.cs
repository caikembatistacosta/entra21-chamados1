using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SingleResponseFactory<T>
    {
        private static SingleResponseFactory<T> _factory;

        public static SingleResponseFactory<T> CreateInstance()
        {
            _factory ??= new SingleResponseFactory<T>();
            return _factory;
        }
        public SingleResponse<T> CreateSuccessSingleResponse(T item)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Item = item,
            };
        }
        public AuthenticateResponse<T> CreateSuccessAuthenticateResponse(T item, string token)
        {
            return new AuthenticateResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Item = item,
                Token = token
            };
        }
        public AuthenticateResponse<T> CreateFailureAuthenticateResponse()
        {
            return new AuthenticateResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou"
            };
        }
        public AuthenticateResponse<T> CreateFailureAuthenticateResponse(Exception ex)
        {
            return new AuthenticateResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex
            };
        }
        public SingleResponse<T> CreateFailureSingleResponse(Exception ex)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex,
            };

        }
        public SingleResponse<T> CreateFailureSingleResponse()
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
            };
        }
        public SingleResponse<T> CreateFailureSingleResponse(string message)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = message,
            };
        }
    }
}
