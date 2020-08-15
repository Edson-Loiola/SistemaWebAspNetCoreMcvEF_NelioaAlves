using System;

namespace SalesWebMvc.Services.Excpetions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        { 
        }

        //exception personalizada
    }
}
