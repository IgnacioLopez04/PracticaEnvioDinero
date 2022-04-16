using Entidades;

namespace PracticaEnvioDinero
{
    public class Logica
    {
        private static int Contador { get; set; }
        List<Usuario> Usuarios = new List<Usuario>();

        #region Singleton
        private static Logica intancia = null;

        private Logica() { }

        public static Logica Instance
        {
            get
            {
                if (intancia == null)
                {
                    intancia = new Logica();
                }
                return intancia;
            }
        }
        #endregion

        public string Post(int dniEmisor, int dniReceptor, string descripcion, decimal monto)
        {
            if (Validar(dniEmisor, dniEmisor) && Validar(dniEmisor, monto))
            {
                int id = AumentarContador();

                CargarMoviento(id, descripcion, monto * (-1), dniEmisor);
                CargarMoviento(id, descripcion, monto, dniReceptor);
                return "201";
            }
            throw new Exception("Error 400; No se encontro a los usuarios"); 
        }

        public string Delete(int id)
        {
            foreach (var usuario in Usuarios)
            {
                if (usuario.HistoricoMovimiento.Exists(x => x.Id == id))
                {
                    int identificador = AumentarContador();
                    Movimiento movimiento = usuario.HistoricoMovimiento.Find(x => x.Id == id);

                    CargarMoviento(identificador, "Cancelacion. " + movimiento.Descripcion, movimiento.Monto * (-1), usuario.Dni);
                }
            }
            return "No se pudo cancelar el movimiento";
        }

        public List<Movimiento> ListaHistoricoMovimientoPersona(int dni)
        {
            try
            {
               return RetornarUsuario(dni).HistoricoMovimiento.OrderByDescending(x => x.FechaMovimiento).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error 404.");
            }
        }

        private bool CargarMoviento(int id, string descripcion, decimal monto, int dni)
        {
            Movimiento movimiento = new Movimiento(id, descripcion, monto);
            Usuario usuario = RetornarUsuario(dni);
            usuario.CargarHistoricoMovimiento(movimiento);
            return true;
        }
        private Usuario RetornarUsuario(int dni)
        {
            return Usuarios.Find(x => x.Dni == dni);
        }
        private bool Validar(int dniEmisor, int dniReceptor)
        {
            return (Usuarios.Exists(x => x.Dni == dniEmisor) && Usuarios.Exists(x => x.Dni == dniReceptor));
        }

        private bool Validar(int dniEmisor, decimal monto)
        {
            return (Usuarios.Find(x => x.Dni == dniEmisor).Saldo >= monto);
        }

        private int AumentarContador()
        {
            return (Contador += 1);
        }

    }
}