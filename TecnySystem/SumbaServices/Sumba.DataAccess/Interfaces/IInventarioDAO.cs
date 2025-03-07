using MySqlConnector;
using TecnySystem.Models;

namespace TecnySystem.SumbaServices.Sumba.DataAccess.Interfaces
{
    public interface IInventarioDAO
    {
        List<Prenda> ListarInventario(MySqlConnection cn);
    }
}
