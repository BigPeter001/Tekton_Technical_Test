using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Percentaje
    {
        //"createdAt":"2024-01-07T12:13:02.068Z","descripcion":"incidunt voluptatum illum","porcentaje":"5","id":"1"
        public string id { get; set; }
        public string descripcion { get; set; }
        public string porcentaje { get; set; }
        public DateTime createdAt { get; set; }
    }
}
