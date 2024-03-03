using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Management_API.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string msg) : base(msg)
        {

        }

    }
}