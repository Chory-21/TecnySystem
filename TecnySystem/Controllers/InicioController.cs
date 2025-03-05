using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TecnySystem.Data;
using TecnySystem.Models;

namespace TecnySystem.Controllers
{
    public class InicioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHistorialProductos()
        {
            try
            {
                var historial = await _context.Prendas
                    .Include(p => p.DescPrenda)
                    .Include(p => p.PrendasFallas)
                    .Select(p => new
                    {
                        p.CodigoPrenda,
                        p.DescPrenda.CodigoLote,
                        p.DescPrenda.Categoria,
                        p.DescPrenda.Modelo,
                        p.DescPrenda.Color,
                        p.DescPrenda.FechaRegistro,
                        CatFTela = p.PrendasFallas.FirstOrDefault().CatFTela,
                        DescFTela = p.PrendasFallas.FirstOrDefault().DescFTela,
                        EstadoFTela = p.PrendasFallas.FirstOrDefault().EstadoFTela,
                        CatFLavanderia = p.PrendasFallas.FirstOrDefault().CatFLavanderia,
                        DescFLavanderia = p.PrendasFallas.FirstOrDefault().DescFLavanderia,
                        EstadoFLavanderia = p.PrendasFallas.FirstOrDefault().EstadoFLavanderia,
                        Total = p.DescPrenda.Tallas.Sum(t => t.Cantidad)

                    })
                    .ToListAsync();

                return Json(new { data = historial });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EliminarPrenda(string codigoPrenda)
        {
            try
            {
                var prenda = await _context.Prendas.FirstOrDefaultAsync(p => p.CodigoPrenda == codigoPrenda);
                if (prenda != null)
                {
                    _context.Prendas.Remove(prenda);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Prenda no encontrada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
