using Ardalis.Specification;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.specifications
{
    public class PagedProductsSpecification : Specification<Product>
    {
        public PagedProductsSpecification(int pageSize, int pageNumber,string name, string statusName, string description )
        {
            Query.Skip( (pageNumber-1) * pageSize )
                .Take(pageSize);

            if (!string.IsNullOrEmpty(name))
                Query.Search(x => x.Name, "%" + name + "%");

            if (!string.IsNullOrEmpty(statusName))
                Query.Search(x => x.StatusName, "%" + statusName + "%");

            if (!string.IsNullOrEmpty(description))
                Query.Search(x => x.Description, "%" + description + "%");
        }
    }
}
