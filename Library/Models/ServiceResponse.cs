using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public interface IServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ServiceResponse : IServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }
}
