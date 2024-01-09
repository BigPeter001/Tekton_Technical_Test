using Application.parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.features.products.queries.getProductAll
{
    public class GetAllProductsParameters : RequestParameter
    {
        public string? Name {  get; set; }
        public string? StatusName { get; set; }

        public string? Description { get; set; }

    }
}
