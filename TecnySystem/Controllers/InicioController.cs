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
using TecnySystem.SumbaServices.Sumba.Business.Interfaces;

namespace TecnySystem.Controllers
{
    public class InicioController : Controller
    {
        private readonly IInventarioNeg _inventarioNeg;

        public InicioController(IInventarioNeg inventarioNeg)
        {
            _inventarioNeg = inventarioNeg;
        }

        public IActionResult Inicio()
        {
            return View();
        }

        public JsonResult GetInventario()
        {
            try
            {
                Console.WriteLine("Ejecutando GetInventario...");
                List<Prenda> lista = _inventarioNeg.ListarInventario();
                Console.WriteLine($"Datos recibidos en GetInventario: {lista.Count} registros");

                return Json(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetInventario: " + ex.Message);
                return Json(new { error = ex.Message });
            }
        }


    }
}
