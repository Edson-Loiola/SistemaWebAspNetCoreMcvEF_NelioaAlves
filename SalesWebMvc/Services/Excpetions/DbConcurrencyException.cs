using System;


namespace SalesWebMvc.Services.Excpetions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message) : base(message)
        { 
        }
    }
}
