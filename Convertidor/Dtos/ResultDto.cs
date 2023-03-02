using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Convertidor.Dtos
{
    public class ResultDto<T>
    {

        public ResultDto(T data)
        {
            Data = data;

        }

        public T? Data { get; set; }
        public bool IsValid  { get; set; }
        public string Message { get; set; } = string.Empty;
    

    }


   

}
