using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class ImageTypeException : Exception
    {
        public ImageTypeException(string? message) : base(message)
        {
        }
    }
}
