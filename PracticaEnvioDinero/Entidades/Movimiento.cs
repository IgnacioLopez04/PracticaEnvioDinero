using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Movimiento
    {
        public int Id { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public Movimiento()
        {

        }

        public Movimiento(int id, string descripcion, decimal monto)
        {
            Id = id;
            FechaMovimiento = DateTime.Now;
            Descripcion = descripcion;
            Monto = monto;
        }

    }
}
