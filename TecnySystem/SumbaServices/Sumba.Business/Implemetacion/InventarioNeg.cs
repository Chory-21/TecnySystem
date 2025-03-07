using MySqlConnector;
using TecnySystem.Models;
using TecnySystem.SumbaServices.Sumba.Business.Interfaces;
using TecnySystem.SumbaServices.Sumba.DataAccess.Interfaces;

namespace TecnySystem.SumbaServices.Sumba.Business.Implemetacion
{
    public class InventarioNeg : IInventarioNeg
    {
        private readonly IInventarioDAO _inventarioDAO;

        public InventarioNeg(IInventarioDAO inventarioDAO)
        {
            _inventarioDAO = inventarioDAO;
        }

        public List<Prenda> ListarInventario()
        {
            List<Prenda> listar = null;
            using (var con = new MySqlConnection())
            {
                try
                {
                    con.Open();
                    listar = _inventarioDAO.ListarInventario(con);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return listar;
        }
    }
}
