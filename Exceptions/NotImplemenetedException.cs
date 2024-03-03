using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Management_API.Exceptions
{
    public class NotImplemenetedException : Exception
    {
        public NotImplemenetedException(string msg) : base(msg)
        {

        }

    }
}