using System.Data;
using Dapper;
using MySqlConnector;
using TecnySystem.Models;
using TecnySystem.SumbaServices.Sumba.DataAccess.Interfaces;

namespace TecnySystem.SumbaServices.Sumba.DataAccess.Implementacion
{
    public class InventarioDAO : IInventarioDAO
    {
        private readonly string _connectionString;

        public InventarioDAO(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public List<Prenda> ListarInventario(MySqlConnection cn)
        {
            List<Prenda> prendas = new List<Prenda>();

            string query = @"
        SELECT `p`.`id_prenda`, `p`.`codigo_prenda`, `p`.`id_desc_prenda`, `d`.`id_desc_prenda`, `d`.`categoria`, `d`.`codigo_lote`, `d`.`color`, `d`.`fecha_registro`, `d`.`modelo`
      FROM `prendas` AS `p`
      INNER JOIN `desc_prendas` AS `d` ON `p`.`id_desc_prenda` = `d`.`id_desc_prenda`
      ORDER BY `p`.`id_prenda`, `d`.`id_desc_prenda`";



            using (var cmd = new MySqlCommand(query, cn))
            {
                cmd.CommandType = CommandType.Text;
                MySqlDataReader dr = cmd.ExecuteReader();

                Dictionary<string, Prenda> prendaDict = new Dictionary<string, Prenda>();

                if (dr != null)
                {
                    int posCodigoPrenda = dr.GetOrdinal("codigo_prenda");
                    int posCodigoLote = dr.GetOrdinal("codigo_lote");
                    int posCategoria = dr.GetOrdinal("categoria");
                    int posModelo = dr.GetOrdinal("modelo");
                    int posColor = dr.GetOrdinal("color");
                    int posFechaRegistro = dr.GetOrdinal("fecha_registro");
                    int posTalla = dr.GetOrdinal("talla");
                    int posCantidad = dr.GetOrdinal("cantidad");

                    while (dr.Read())
                    {
                        string codigoPrenda = dr.IsDBNull(posCodigoPrenda) ? "N/A" : dr.GetString(posCodigoPrenda);

                        // Si la prenda no existe en el diccionario, la creamos
                        if (!prendaDict.ContainsKey(codigoPrenda))
                        {
                            Prenda prenda = new Prenda
                            {
                                codigo_prenda = codigoPrenda,
                                DescPrenda = new Desc_Prendas // Usar el nombre correcto
                                {
                                    codigo_lote = dr.IsDBNull(posCodigoLote) ? "N/A" : dr.GetString(posCodigoLote),
                                    categoria = dr.IsDBNull(posCategoria) ? "N/A" : dr.GetString(posCategoria),
                                    modelo = dr.IsDBNull(posModelo) ? "N/A" : dr.GetString(posModelo),
                                    color = dr.IsDBNull(posColor) ? "N/A" : dr.GetString(posColor),
                                    fecha_registro = dr.IsDBNull(posFechaRegistro) ? DateTime.Now : dr.GetDateTime(posFechaRegistro),
                                    Tallas = new List<Talla>() // Inicializamos la lista de tallas
                                }
                            };
                            prendaDict[codigoPrenda] = prenda;
                        }

                        // Agregar la talla a la prenda correspondiente
                        if (!dr.IsDBNull(posTalla) && !dr.IsDBNull(posCantidad))
                        {
                            prendaDict[codigoPrenda].DescPrenda.Tallas.Add(new Talla
                            {
                                talla = dr.GetString(posTalla),
                                cantidad = dr.GetInt32(posCantidad)
                            });
                        }
                    }
                    dr.Close();
                }

                prendas = prendaDict.Values.ToList(); // Convertir el diccionario en una lista final
            }

            return prendas;
        }



    }
}

