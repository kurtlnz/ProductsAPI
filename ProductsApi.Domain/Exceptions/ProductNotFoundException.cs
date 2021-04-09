using System;

namespace XeroTechnicalTest.Domain.Exceptions
{
    public class ProductNotFoundException : ObjectNotFoundException
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message)
            : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}