using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services.Excpetions
{
    public class IntregityException :ApplicationException //para criar tratamentos de execptions sempre herdar dessa classe
    {
        public IntregityException(string message) : base(message) //repassar para a super classe
        {             
        }
    }
}
