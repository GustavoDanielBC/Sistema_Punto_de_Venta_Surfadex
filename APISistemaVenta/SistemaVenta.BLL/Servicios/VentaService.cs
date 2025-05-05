using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IventaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleventaRepositorio;
        private readonly IMapper _mapper;

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            var listaResultado = new List<Venta>();

            try
            {

                if(buscarPor == "fecha")
                {
                    DateTime fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
                    DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

                    listaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fechInicio.Date &&
                    v.FechaRegistro.Value.Date <= fechFin.Date
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
                else
                {
                    listaResultado = await query.Where(v => v.FolioFiscal == numeroVenta
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }


            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<VentaDTO>>(listaResultado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleventaRepositorio.Consultar();
            var listaResultado = new List<DetalleVenta>();

            try
            {
                DateTime fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
                DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

                listaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv =>
                    dv.IdVentaNavigation.FechaRegistro.Value.Date >= fechInicio.Date &&
                    dv.IdVentaNavigation.FechaRegistro.Value.Date <= fechFin.Date
                    ).ToListAsync();


            }
            catch
            {
                throw; 
            }

            return _mapper.Map<List<ReporteDTO>>(listaResultado);
        }
    }
}
