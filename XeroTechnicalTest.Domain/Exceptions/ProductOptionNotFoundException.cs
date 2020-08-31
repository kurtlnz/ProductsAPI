using System;

namespace XeroTechnicalTest.Domain.Exceptions
{
    public class ProductOptionNotFoundException : Exception
    {
        public ProductOptionNotFoundException()
        {
        }

        public ProductOptionNotFoundException(string message)
            : base(message)
        {
        }

        public ProductOptionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}