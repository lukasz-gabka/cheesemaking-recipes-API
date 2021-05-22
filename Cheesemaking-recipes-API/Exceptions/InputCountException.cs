using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cheesemaking_recipes_API.Exceptions
{
    public class InputCountException : Exception
    {
        public InputCountException(string message) : base(message)
        {

        }
    }
}
