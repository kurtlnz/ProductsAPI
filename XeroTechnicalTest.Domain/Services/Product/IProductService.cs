namespace XeroTechnicalTest.Domain.Services
{
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        void CreateProduct();
        
        /// <summary>
        /// 
        /// </summary>
        void GetProduct(Guid id);
        
        /// <summary>
        /// 
        /// </summary>
        void UpdateProduct();

        /// <summary>
        /// 
        /// </summary>
        void DeleteProduct(Guid id);
    }
}