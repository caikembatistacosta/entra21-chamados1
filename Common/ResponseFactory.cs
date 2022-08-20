using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResponseFactory
    {
        public static Response CreateSuccessResponse()
        {
            return new Response()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso"
            };
        }
        public static Response CreateFailureResponse(Exception ex)
        {
            return new Response()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex
            };
        }
    }
}
