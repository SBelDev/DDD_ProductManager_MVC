using Core.Services;
using DAL;
using System.Collections.Generic;
using System;
using Core.Mail;

namespace Core.Mail
{
    public interface ISendEntity<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetProductForSend();

        void UpdateProduct(TEntity product);
    }


    public class SendProduct : ISendEntity<Product>
    {
        private IService<Product> productService;

        public SendProduct(IService<Product> productService)
        {
            this.productService = productService;
        }

        public IEnumerable<Product> GetProductForSend()
        {
            return productService.Get(filter: x => x.IsSendByEmail == false);
        }

        public void UpdateProduct(Product product)
        {
             productService.Update(product);
        }
    }
}
