using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private ProductModelContext dataContext;

        public ProductModelContext Init()
        {
            return dataContext ?? (dataContext = new ProductModelContext());
        }
    }
}
