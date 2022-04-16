namespace Entidades
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public decimal Saldo { get; set; }
        public List<Movimiento> HistoricoMovimiento { get; set; }

        public bool CargarHistoricoMovimiento(Movimiento movimiento)
        {
            HistoricoMovimiento.Add(movimiento);
            return true;
        }
    }
}